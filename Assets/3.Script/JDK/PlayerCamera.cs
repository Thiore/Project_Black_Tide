using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private InputManager input;

    [Header("ī�޶� ���� ���� ������Ƽ")]
    [Range(0,10)]
    [SerializeField] private float cameraSpeedX; // ���� ī�޶� ����
    [Range(0, 10)]
    [SerializeField] private float cameraSpeedY; // ���� ī�޶� ����

    private Vector2 lastTouchPosition; // ���������� ��ġ�� ��ġ

    private Vector3 deltaRot;

    //private void Awake()
    //{
       

       
    //}
    private void Start()
    {
        input = InputManager.Instance;
        lastTouchPosition = Vector2.zero;
        deltaRot = Vector3.zero;
        if (cameraSpeedX.Equals(0f))
            cameraSpeedX = 5f;
        if (cameraSpeedY.Equals(0f))
            cameraSpeedY = 5f;
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
        
        transform.Rotate(deltaRot*Time.fixedDeltaTime*5f);
        deltaRot = Vector3.zero;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
    }
}