using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private InputManager input;

    [Header("ī�޶� ���� ���� ������Ƽ")]
    [Range(1,10)]
    [SerializeField] private float cameraSpeedX; // ���� ī�޶� ����
    [Range(1, 10)]
    [SerializeField] private float cameraSpeedY; // ���� ī�޶� ����

    private Vector2 lastTouchPosition; // ���������� ��ġ�� ��ġ

    private Vector3 deltaRot;
    //private CinemachinePOV pov; // Cinemachine POV ������Ʈ ����
    //private CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera ������Ʈ ����
    //private CinemachineInputProvider inputProvider;

    private void Awake()
    {
        input = InputManager.Instance;

        // ������Ʈ�� �ʱ�ȭ
        //TryGetComponent(out virtualCamera);
        //pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
       
    }
    private void Start()
    {
        lastTouchPosition = Vector2.zero;
        deltaRot = Vector3.zero;
        if (cameraSpeedX.Equals(0f))
            cameraSpeedX = 50f;
        if (cameraSpeedY.Equals(0f))
            cameraSpeedY = 50f;
        //TryGetComponent(out inputProvider);
    }

    private void Update()
    {
        if (!input.lookData.value.Equals(Vector2.zero))
        {
            Vector2 currentTouchPosition = input.lookData.value;
            if (lastTouchPosition.Equals(Vector2.zero))
            {
                lastTouchPosition = currentTouchPosition; // ������ ��ġ ��ġ ���
                return;
            }
            Vector2 delta = currentTouchPosition - lastTouchPosition;
            deltaRot = new Vector3(-delta.y * cameraSpeedY, delta.x * cameraSpeedX);
            lastTouchPosition = currentTouchPosition;
        }
        else
        {
            if (!lastTouchPosition.Equals(Vector2.zero))
            {
                // ī�޶� ȸ�� ������ 0���� �����Ͽ� ȸ���� ����
                lastTouchPosition = Vector2.zero;
            }

        }
    }
    private void FixedUpdate()
    {
        
        transform.Rotate(deltaRot*Time.fixedDeltaTime*10f);
        deltaRot = Vector3.zero;
    }
}