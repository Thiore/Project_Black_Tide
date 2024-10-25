using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public RectTransform leftArea; // UI ������ LeftArea
    public RectTransform joystickArea; // Joystick Area
    public LayerMask touchableLayer; // ��ġ ������ ���̾�

    private Vector2 touchStartPos; // ó�� ��ġ�� ��ġ
    private Vector2 touchCurPos;   // ���� ��ġ ��ġ
    public bool isTouching = false; // ��ġ ������ ����

    public Vector2 MoveInput;

    private void Update()
    {
        // ��ġ��ũ���� �����ϴ��� Ȯ��
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            // ��ġ�� ���۵Ǿ��� ��
            if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                HandleTouchBegan(touchPosition);
            }

            // ��ġ ���� �� (�հ����� �����̴� ���)
            if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved && isTouching)
            {
                HandleTouchMoved(touchPosition);
            }
        }

        // ��ġ�� ������ ��
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            MoveInput = new Vector2(0, 0);
            isTouching = false; // ��ġ�� ������ false�� ����
        }
    }

    // ��ġ�� ���۵� �� ȣ��Ǵ� �޼���
    private void HandleTouchBegan(Vector2 touchPosition)
    {
        // Joystick Area�� ��ġ�ߴ��� Ȯ�� (�ٷ� ��ġ ���� ��ġ ����)
        if (RectTransformUtility.RectangleContainsScreenPoint(joystickArea, touchPosition))
        {
            // Joystick Area �ȿ����� �ٷ� ��ġ ���� ��ġ�� ����
            touchStartPos = touchPosition;
            isTouching = true; // ��ġ �� ���·� ����
        }
        // LeftArea�� ��ġ�ߴ��� Ȯ��
        else if (RectTransformUtility.RectangleContainsScreenPoint(leftArea, touchPosition))
        {
            // ��ġ�� ��ġ�� Touchable Object�� �ִ��� Ȯ��
            if (!IsTouchableObjectAtPosition(touchPosition))
            {
                // ��ġ ���� ��ġ�� ����
                touchStartPos = touchPosition;
                isTouching = true; // ��ġ �� ���·� ����
            }
        }
    }

    // ��ġ�� ���� "Touchable Object"�� �ִ��� Ȯ���ϴ� �޼���
    private bool IsTouchableObjectAtPosition(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchableLayer))
        {
            // "Touchable Object" �±׸� ���� ������Ʈ�� �ִ��� Ȯ��
            if (hit.collider.CompareTag("touchableobject"))
            {
                return true;
            }
        }
        return false;
    }

    // ��ġ�� �̵����� �� ȣ��Ǵ� �޼���
    private void HandleTouchMoved(Vector2 touchPosition)
    {
        // ���� ��ġ ��ġ�� ����
        touchCurPos = touchPosition;

        // ��ġ ���� ��ġ�� ���� ��ġ�� ���� ��ȯ
        Vector2 touchDelta = GetTouchDelta();
        MoveInput = touchDelta.normalized;
    }

    // touchCurPos - touchStartPos ���� ��ȯ�ϴ� �޼���
    public Vector2 GetTouchDelta()
    {
        return touchCurPos - touchStartPos;
    }
}
