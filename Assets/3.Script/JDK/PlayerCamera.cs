using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("ī�޶� ���� ���� ������Ƽ")]
    [SerializeField] private float camera_speed_x = 0;
    [SerializeField] private float camera_speed_y = 0;

    [SerializeField] private RectTransform rect = null;

    private Vector2 last_touch_position;

    private CinemachinePOV pov = null;
    private CinemachineVirtualCamera virtual_camera = null;

    private bool isDragging = false;

    private void Awake()
    {
        virtual_camera = this.GetComponent<CinemachineVirtualCamera>();
        pov = virtual_camera.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Update()
    {
        // SetCameraSpeed(camera_speed_x, camera_speed_y);

        bool contains = RectTransformUtility.RectangleContainsScreenPoint(rect, last_touch_position);

        if (contains)
        {
            if (Touchscreen.current.touches.Count > 0)
            {
                var touch = Touchscreen.current.primaryTouch;

                if (touch.press.isPressed)
                {
                    if (!isDragging)
                    {
                        last_touch_position = touch.position.ReadValue();
                        isDragging = true;
                    }
                    else
                    {
                        Vector2 currentTouchPosition = touch.position.ReadValue();
                        Vector2 delta = currentTouchPosition - last_touch_position;

                        // POV ī�޶� delta �� ����
                        pov.m_HorizontalAxis.Value += delta.x * camera_speed_x;
                        pov.m_VerticalAxis.Value -= delta.y * camera_speed_y;

                        last_touch_position = currentTouchPosition; // ������ ��ġ ��ġ ������Ʈ
                    }
                }
                else
                {
                    isDragging = false; // ��ġ�� ������ �巡�� ����
                }
            }
        }
    }

    /* private void SetCameraSpeed(float horizontal_speed, float vertical_speed) // Pov ��������
    {
        pov.m_HorizontalAxis.m_MaxSpeed = horizontal_speed; // ����
        pov.m_VerticalAxis.m_MaxSpeed = vertical_speed; // ����
    } */
}