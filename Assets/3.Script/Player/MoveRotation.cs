using UnityEngine;
using UnityEngine.InputSystem;

public class MoveRotation : MonoBehaviour
{
    public RectTransform leftArea; // UI ������ LeftArea
    private Rigidbody rb;
    public float rotationSpeed = 5f; // ȸ�� �ӵ� ����
    private float rotationAmount;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ��ġ�� ���� ������ Ȯ��
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {

            if (!RectTransformUtility.RectangleContainsScreenPoint(leftArea, Touchscreen.current.primaryTouch.position.ReadValue()))
            {
                // ��ġ �̵�(delta) �� ��������
                Vector2 touchDelta = Touchscreen.current.primaryTouch.delta.ReadValue();

                // deltaX�� ����� ȸ�� �� ���
                rotationAmount = touchDelta.x * rotationSpeed;
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (rotationAmount != 0)
        {
            // ȸ�� ����
            RotateObject(rotationAmount);
            rotationAmount = 0; // ȸ�� ���� �� �ʱ�ȭ
        }
    }

    private void RotateObject(float deltaX)
    {
        Quaternion deltaRotation = Quaternion.Euler(0f, deltaX, 0f);

        // Rigidbody�� ȸ���� ���� (���� ȸ�� ���� �߰�)
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
