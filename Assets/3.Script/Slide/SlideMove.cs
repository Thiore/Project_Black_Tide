using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlideMove : MonoBehaviour
{
    public LayerMask touchableLayer; // ��ġ ������ ���̾� ����
    private GameObject selectedObject; // ���� ��ġ�� ������Ʈ
    private bool isObjectSelected;
    private Vector3 initialObjectPosition; // ��ġ ���� �� ������Ʈ�� �ʱ� ��ġ
    private Vector2 initialTouchPosition;  // ��ġ ���� �� �հ��� ��ġ
    private Vector3 lastValidPosition; // ���������� ��ȿ�ߴ� ��ġ ����

    public GameObject correctZone;

    private void Update()
    {
        if (isObjectSelected)
        {
            // ������Ʈ�� ���õ� ���¿��� �հ��� �̵��� ���� ������Ʈ�� ������
            MoveObjectWithTouch();
        }
        else
        {
            // ��ġ�� ���۵Ǹ� ������Ʈ ���� �� �̵� ���� ���� Ȯ��
            if (DetectTouchStart())
            {
                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                if (DetectObjectAtTouch(touchPosition)) // ������ ������Ʈ�� ��ġ ������ ���̾ ���� ��
                {
                    isObjectSelected = true;
                }
            }
        }

        // ��ġ�� ����Ǹ� ���� ����
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            if (selectedObject != null)
            {
                Debug.Log($"Selected object name: {selectedObject.gameObject.name}");
                selectedObject.GetComponent<Outline>().enabled = false;
                selectedObject = null;
                isObjectSelected = false;
                correctZone.GetComponent<CorrectCheck>().CheckAllRays();
            }
        }
    }

    private bool DetectTouchStart()
    {
        // ��ġ��ũ���� �ִ��� Ȯ���ϰ�, ��ġ�� ���۵Ǿ����� Ȯ��
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            // ��ġ�� ���۵Ǿ����� Ȯ��
            var touchPhase = Touchscreen.current.primaryTouch.phase.ReadValue();
            if (touchPhase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }

    private bool DetectObjectAtTouch(Vector2 touchPosition)
    {
        // ��ġ ��ġ�� ���� ��ǥ�� ��ȯ
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        // ������ ���̾��� ������Ʈ�� Raycast �浹 ���� Ȯ��
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchableLayer))
        {
            selectedObject = hit.collider.gameObject;
            selectedObject.GetComponent<Outline>().enabled = true;
            initialObjectPosition = selectedObject.transform.position; // ������Ʈ�� �ʱ� ��ġ ����
            initialTouchPosition = touchPosition; // ��ġ ���� ��ġ ����
            return true; // ��ġ ������ ���̾ �ִ� ������Ʈ�� ������ ��� true ��ȯ
        }
        return false; // ��ġ ������ ������Ʈ�� ���� ��� false ��ȯ
    }

    private void MoveObjectWithTouch()
    {
        if (selectedObject == null) return;

        // ��ġ �̵��� ���� ������Ʈ �̵�
        if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            Vector2 currentTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            MoveObject(currentTouchPosition);
        }
    }


    private void MoveObject(Vector2 currentTouchPosition)
    {
        // ��ġ ��ġ�� ���� ��ǥ�� ��ȯ
        Ray ray = Camera.main.ScreenPointToRay(currentTouchPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            // �̵��� ��ǥ ��ġ ���
            Vector3 targetPosition = new Vector3(
                Mathf.Round(hitInfo.point.x * 2) * 0.5f,
                selectedObject.transform.position.y,
                Mathf.Round(hitInfo.point.z * 2) * 0.5f
            );

            // SlideObject ������Ʈ ��������
            SlideObject slideObject = selectedObject.GetComponent<SlideObject>();
            Vector3 currentPosition = selectedObject.transform.position;
            if (slideObject != null && !slideObject.IsOverlappingAtPosition(targetPosition))
            {
                // ��ġ�� ���� ���� �̵�
                selectedObject.transform.position = targetPosition;
            }
            else
            {
                // ��ġ�� ��� �ʱ� ��ġ�� ����
                selectedObject.transform.position = currentPosition;
                //selectedObject.GetComponent<Outline>().enabled=false;
                //selectedObject = null;
                //isObjectSelected = false;
                Debug.Log("Cannot move, object is overlapping with another collider.");
            }
        }
    }



}
