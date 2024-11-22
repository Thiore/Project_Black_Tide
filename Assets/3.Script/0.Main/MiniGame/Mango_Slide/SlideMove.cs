 using UnityEngine;
 using UnityEngine.InputSystem;

public class SlideMove : MonoBehaviour
{
    public LayerMask touchableLayer; // ��ġ ������ ���̾� ����
    private GameObject selectedObject; // ���� ��ġ�� ������Ʈ
    private bool isObjectSelected;
    private Vector3 initialObjectPosition; // ��ġ ���� �� ������Ʈ�� �ʱ� ��ġ
    private Vector2 initialTouchPosition;  // ��ġ ���� �� �հ��� ��ġ
    public GameObject correctZone;

    private void Update()
    {
        if (isObjectSelected)
        {
            // ������Ʈ�� ���õ� ���¿��� �հ��� �̵��� ���� ������Ʈ�� 0.5 ������ �̵�
            MoveObjectWithTouchX();
            MoveObjectWithTouchZ();
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
                    initialTouchPosition = touchPosition; // ��ġ ���� ��ġ ����
                    initialObjectPosition = selectedObject.transform.position; // ������Ʈ�� �ʱ� ��ġ ����
                }
            }
        }

        // ��ġ�� ����Ǹ� ���� ����
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            if (selectedObject != null)
            {
                selectedObject.GetComponent<Outline>().enabled = false;
                selectedObject = null;
                isObjectSelected = false;
                correctZone.GetComponent<CorrectCheck>().CheckAllRays();
            }
        }
    }

    private bool DetectTouchStart()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            var touchPhase = Touchscreen.current.primaryTouch.phase.ReadValue();
            return touchPhase == UnityEngine.InputSystem.TouchPhase.Began;
        }
        return false;
    }

    private bool DetectObjectAtTouch(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, touchableLayer))
        {
            selectedObject = hit.collider.gameObject;
            selectedObject.GetComponent<Outline>().enabled = true;
            return true;
        }
        return false;
    }

    private void MoveObjectWithTouchX()
    {
        if (selectedObject == null) return;

        // ���� ��ġ ��ġ ��������
        Vector2 currentTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        // �̵��� X ������ ���
        Vector3 moveOffset = CalculateMoveOffsetX(currentTouchPosition);

        // X �������� ��ǥ ��ġ ���
        Vector3 targetPositionX = initialObjectPosition + new Vector3(moveOffset.x, 0, 0);

        // SlideObject ������Ʈ �����ͼ� ��ħ ���� Ȯ��
        SlideObject slideObject = selectedObject.GetComponent<SlideObject>();
        if (slideObject != null && !slideObject.IsOverlappingAtPosition(targetPositionX))
        {
            // ��ġ�� ���� ���� X�� �̵�
            selectedObject.transform.position = targetPositionX;
            initialObjectPosition = targetPositionX; // �̵� �� �ʱ� ��ġ ������Ʈ
            Debug.Log("X�� �̵�");
        }
        else
        {
            initialTouchPosition.y = currentTouchPosition.y;
        }
    }

    private void MoveObjectWithTouchZ()
    {
        if (selectedObject == null) return;

        // ���� ��ġ ��ġ ��������
        Vector2 currentTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        // �̵��� Z ������ ���
        Vector3 moveOffset = CalculateMoveOffsetZ(currentTouchPosition);

        // Z �������� ��ǥ ��ġ ���
        Vector3 targetPositionZ = initialObjectPosition + new Vector3(0, 0, moveOffset.z);

        // SlideObject ������Ʈ �����ͼ� ��ħ ���� Ȯ��
        SlideObject slideObject = selectedObject.GetComponent<SlideObject>();
        if (slideObject != null && !slideObject.IsOverlappingAtPosition(targetPositionZ))
        {
            // ��ġ�� ���� ���� Z�� �̵�
            selectedObject.transform.position = targetPositionZ;
            initialObjectPosition = targetPositionZ; // �̵� �� �ʱ� ��ġ ������Ʈ
            Debug.Log("Z�� �̵�");
        }
        else
        {
            initialTouchPosition.x = currentTouchPosition.x;
        }
    }

    private Vector3 CalculateMoveOffsetX(Vector2 currentTouchPosition)
    {
        Vector3 moveOffset = Vector3.zero;
        Vector2 touchDelta = currentTouchPosition - initialTouchPosition;

        // X ������ �̵� ����
        if (Mathf.Abs(touchDelta.y) >= 0.1f)
        {
            moveOffset.x = -Mathf.Sign(touchDelta.y) * 0.25f;
            initialTouchPosition.y = currentTouchPosition.y; // �̵� �� ���ο� ������ ����
        }

        return moveOffset;
    }

    private Vector3 CalculateMoveOffsetZ(Vector2 currentTouchPosition)
    {
        Vector3 moveOffset = Vector3.zero;
        Vector2 touchDelta = currentTouchPosition - initialTouchPosition;

        // Z ������ �̵� ����
        if (Mathf.Abs(touchDelta.x) >= 0.1f)
        {
            moveOffset.z = Mathf.Sign(touchDelta.x) * 0.25f;
            initialTouchPosition.x = currentTouchPosition.x; // �̵� �� ���ο� ������ ����
        }

        return moveOffset;
    }
}
