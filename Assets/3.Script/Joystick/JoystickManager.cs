using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickManager : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject handle;
    [SerializeField] private RectTransform leftArea;
    [SerializeField] private RectTransform joystickArea;

    public LayerMask touchableLayer;
    private Canvas canvas;  // ĵ���� ����
    private Vector2 joystickCenter; // ���̽�ƽ �߽� ��ǥ
    public float maxDistance = 70f; // �ڵ��� ������ �� �ִ� �ִ� �Ÿ�
    private bool joystickActive; // ���̽�ƽ Ȱ��ȭ ����

    private void Start()
    {
        // ĵ���� ���� ��������
        canvas = GetComponentInParent<Canvas>();
        joystickActive = false;
        // ���̽�ƽ�� �ʱ⿡�� ��Ȱ��ȭ
        joystick.SetActive(false);
    }

    private void Update()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            // ��ġ ���¿� ���� ó��
            if (joystickActive)
            {
                HandleMove();
            }
            else
            {
                HandleTouchBegan(touchPosition);
            }
        }

        HandleTouchEnded();
    }

    // ��ġ ���� �� ȣ��
    private void HandleTouchBegan(Vector2 touchPosition)
    {
        if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(joystickArea, touchPosition, canvas.worldCamera))
            {
                ActivateJoystick(touchPosition, true); // ���̽�ƽ Ȱ��ȭ
                joystickCenter = joystick.GetComponent<RectTransform>().anchoredPosition; // ���̽�ƽ �߽� ��ǥ ����
                joystickActive = true; // ���̽�ƽ Ȱ��ȭ �÷��� ����
            }else if (RectTransformUtility.RectangleContainsScreenPoint(leftArea, touchPosition, canvas.worldCamera))
            {
                HandleLeftAreaTouch(touchPosition);
            }
        }
    }

    // ��ġ �̵� �� ȣ�� (�հ��� �̵��� ���� �ڵ��� �ٷ� ����)
    private void HandleMove()
    {
            // ���� ��ġ�� ��ġ�� �����ͼ� ���̽�ƽ �߽ɿ��� �ڵ��� �̵���Ŵ
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            MoveHandle(touchPosition); // ��ġ ��ġ�� ���� �ڵ� �̵�
    }

    private void MoveHandle(Vector2 touchPosition)
    {
        // ��ġ�� ��ũ�� ��ǥ�� ���̽�ƽ ��ǥ�� ��ȯ
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystick.GetComponent<RectTransform>(),
            touchPosition,
            canvas.worldCamera,
            out localPoint
        );

        // �ڵ��� ���̽�ƽ �߽ɿ��� ��� �Ÿ��� ���
        Vector2 offset = localPoint;

        // �ڵ��� �ִ� 80f �Ÿ� �̻����� �̵����� �ʵ��� ����
        float maxDistance = 80f;
        if (offset.magnitude > maxDistance)
        {
            offset = offset.normalized * maxDistance; // ������ �����ϵ�, �Ÿ��� 80f�� ����
        }
        Debug.Log($"offset : {offset}");
        // �ڵ��� ��ġ�� ������Ʈ (���̽�ƽ �߽ɿ��� offset��ŭ �̵�)
        handle.GetComponent<RectTransform>().anchoredPosition = offset;
    }

    // ��ġ�� ����Ǿ��� �� ȣ��
    private void HandleTouchEnded()
    {
        if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            handle.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // �ڵ��� �߾����� ����
            ActivateJoystick(Vector2.zero, false); // ���̽�ƽ ��Ȱ��ȭ
            joystickActive = false; // ���̽�ƽ Ȱ��ȭ �÷��� ����
        }
    }

    // LeftArea ��ġ ó��
    private void HandleLeftAreaTouch(Vector2 touchPosition)
    {
        // ��ġ�� ��ġ�� "Touchable Object" �±׸� ���� ������Ʈ�� �ִ��� Ȯ��
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchableLayer))
        {
            if (hit.collider.CompareTag("touchableobject"))
            {
                return;
            }
        }

        // ���̽�ƽ Ȱ��ȭ
        ActivateJoystick(touchPosition, true);
    }

    // ���̽�ƽ Ȱ��ȭ/��Ȱ��ȭ
    private void ActivateJoystick(Vector2 touchPosition, bool isActive)
    {
        if (isActive)
        {

            // ��ġ�� ��ġ�� ĵ���� ��ǥ�� ��ȯ
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                touchPosition,
                canvas.worldCamera,
                out localPoint
            );

            // ���̽�ƽ�� ��ġ�� ��ġ�� ��ġ
            joystick.GetComponent<RectTransform>().anchoredPosition = localPoint; 

            joystickCenter = localPoint; // ���̽�ƽ �߽� ��ǥ ����
            joystick.SetActive(true);
            joystickActive = true;
        }
        else
        {
            joystick.SetActive(false); // ���̽�ƽ ��Ȱ��ȭ
            joystickActive = false;
        }
    }
}
