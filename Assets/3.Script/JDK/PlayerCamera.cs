using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private InputManager input;

    [Header("ī�޶� ���� ���� ������Ƽ")]
    [SerializeField] private float cameraSpeedX = 0; // ���� ī�޶� ����
    [SerializeField] private float cameraSpeedY = 0; // ���� ī�޶� ����

    private Vector2 lastTouchPosition; // ���������� ��ġ�� ��ġ

    private CinemachinePOV pov; // Cinemachine POV ������Ʈ ����
    private CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera ������Ʈ ����
    private CinemachineInputProvider inputProvider;

    private void Awake()
    {
        input = InputManager.Instance;

        // ������Ʈ�� �ʱ�ȭ
        TryGetComponent(out virtualCamera);
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
       
    }
    private void Start()
    {
        lastTouchPosition = Vector2.zero;
        TryGetComponent(out inputProvider);
    }

    private void Update()
    {
        //if (!input.lookData.value.Equals(Vector2.zero))
        //{
        //    Vector2 currentTouchPosition = input.lookData.value;
        //    if (lastTouchPosition.Equals(Vector2.zero))
        //    {
        //        lastTouchPosition = currentTouchPosition; // ������ ��ġ ��ġ ���
        //        return;
        //    }
        //    Vector2 delta = currentTouchPosition - lastTouchPosition;
        //    pov.m_HorizontalAxis.Value += delta.x* cameraSpeedX; // ���� ȸ��
        //    pov.m_VerticalAxis.Value -= delta.y * cameraSpeedY; // ���� ȸ��
        //    lastTouchPosition = currentTouchPosition;
        //}
        //else
        //{
        //    if(!lastTouchPosition.Equals(Vector2.zero))
        //    {
        //        // ī�޶� ȸ�� ������ 0���� �����Ͽ� ȸ���� ����
        //        lastTouchPosition = Vector2.zero;
        //    }
            
        //}
    }
}