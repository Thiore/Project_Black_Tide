using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("ī�޶� ���� ���� ������Ƽ")]
    [SerializeField] private float cameraSpeedX = 0; // ���� ī�޶� ����
    [SerializeField] private float cameraSpeedY = 0; // ���� ī�޶� ����

    [SerializeField] private RectTransform rect = null; // UI ������ �����ϴ� RectTransform

    private Vector2 lastTouchPosition; // ���������� ��ġ�� ��ġ

    private CinemachinePOV pov = null; // Cinemachine POV ������Ʈ ����
    private CinemachineVirtualCamera virtualCamera = null; // Cinemachine Virtual Camera ������Ʈ ����

    private bool isDragging = false; // �巡�� ������ ���θ� ��Ÿ���� �÷���

    private void Awake()
    {
        // ������Ʈ�� �ʱ�ȭ
        virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Update()
    {
        // ��ġ�� �ִ��� Ȯ��
        if (Touchscreen.current.touches.Count > 0)
        {
            var touch = Touchscreen.current.primaryTouch; // ���� ��ġ ������ ������

            if (touch.press.isPressed) // ��ġ�� ���ȴ��� Ȯ��
            {
                if (!isDragging) // �巡�װ� ���۵��� �ʾҴٸ�
                {
                    lastTouchPosition = touch.position.ReadValue(); // ������ ��ġ ��ġ ���
                    isDragging = true; // �巡�� ����
                }
                else // �巡�� ���̶��
                {
                    Vector2 currentTouchPosition = touch.position.ReadValue(); // ���� ��ġ ��ġ
                    Vector2 delta = currentTouchPosition - lastTouchPosition; // ��ġ �̵� �Ÿ� ���

                    // RectTransform ���ο� �ִ��� Ȯ��
                    bool contains = RectTransformUtility.RectangleContainsScreenPoint(rect, lastTouchPosition);

                    if (contains) // RectTransform ���ο��� ��ġ�ϰ� �ִٸ�
                    {
                        // POV ī�޶� delta �� ����
                        pov.m_HorizontalAxis.Value += delta.x * cameraSpeedX; // ���� ȸ��
                        pov.m_VerticalAxis.Value -= delta.y * cameraSpeedY; // ���� ȸ��

                        lastTouchPosition = currentTouchPosition; // ������ ��ġ ��ġ ������Ʈ
                    }
                    else // RectTransform �ܺο��� ��ġ�ϰ� �ִٸ�
                    {
                        // ī�޶� ȸ�� ������ 0���� �����Ͽ� ȸ���� ����
                        pov.m_HorizontalAxis.m_MaxSpeed = 0; // ���� ȸ�� ���� 0���� ����
                        pov.m_VerticalAxis.m_MaxSpeed = 0; // ���� ȸ�� ���� 0���� ����
                    }
                }
            }
            else // ��ġ�� ������
            {
                isDragging = false; // �巡�� ����
            }
        }
    }

    /* 
    // ī�޶� ������ �����ϴ� �޼��� (���� ������ ����)
    private void SetCameraSpeed(float horizontalSpeed, float verticalSpeed)
    {
        pov.m_HorizontalAxis.m_MaxSpeed = horizontalSpeed; // ���� ���� ����
        pov.m_VerticalAxis.m_MaxSpeed = verticalSpeed; // ���� ���� ����
    } 
    */
}