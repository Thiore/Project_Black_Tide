using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemDragLogger : MonoBehaviour
{
    private Vector2 startMousePosition;
    private bool isDragging = false;

    // Input Actions�� �����ϱ� ���� ����
    private Vector2 currentMousePosition;

    public void OnPoint(InputAction.CallbackContext context)
    {
        // ���콺 ��ġ ������Ʈ
        currentMousePosition = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started) // ���콺 Ŭ�� ����
        {
            startMousePosition = currentMousePosition;
            isDragging = true;
        }
        else if (context.canceled && isDragging) // ���콺 Ŭ�� ����
        {
            Vector2 dragVector = currentMousePosition - startMousePosition;

            // �巡�� ���⿡ ���� ó��
            if (Mathf.Abs(dragVector.x) > Mathf.Abs(dragVector.y))
            {
                if (dragVector.x > 0)
                {
                    Debug.Log("���������� �巡�׵Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("�������� �巡�׵Ǿ����ϴ�.");
                }
            }
            else
            {
                if (dragVector.y > 0)
                {
                    Debug.Log("�������� �巡�׵Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("�Ʒ������� �巡�׵Ǿ����ϴ�.");
                }
            }

            isDragging = false;
        }
    }
}