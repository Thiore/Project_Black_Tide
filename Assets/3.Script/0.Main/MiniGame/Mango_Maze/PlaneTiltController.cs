 using System.Collections;
 using UnityEngine;
 using UnityEngine.UI;
 using UnityEngine.InputSystem;

public class PlaneTiltController : MonoBehaviour
{
    [SerializeField] private float tiltMultiplier = 90f; // ���� ������ �����ϴ� ����
    //[SerializeField] private float smoothSpeed = 0.5f; // ȸ���� �ε巯���� �����ϴ� �ӵ�
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject ball;

    private Vector3 startTilt; // �ʱ� ���� ��
    private Quaternion targetRotation; // ��ǥ ȸ�� ��
    private InputAction planeAction;

    private void Awake()
    {
        TryGetComponent(out PlayerInput input);
        planeAction = input.actions.FindAction("Tilt");
    }
    private void OnEnable()
    {
        startTilt = Vector3.zero;
        Debug.Log("enable" + startTilt);
        planeAction.Enable();
        
        planeAction.started += ctx => OnTiltStarted(ctx);
        planeAction.performed += ctx => OnTiltPerformed(ctx);
        

    }
    private void OnDisable()
    {
        planeAction.Disable();

        planeAction.started -= ctx => OnTiltStarted(ctx);
        planeAction.performed -= ctx => OnTiltPerformed(ctx);
        //ball.SetActive(false);
    }

    //private void Start()
    //{
    //    if (Accelerometer.current != null)
    //    {
    //        // ���� ������ ���� ���� �����Ͽ� ���������� ���
    //        startTilt = Accelerometer.current.acceleration.ReadValue();
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Accelerometer not available on this device. Please run on an actual device.");
    //    }
    //}

    private void OnTiltStarted(InputAction.CallbackContext context)
    {
        Debug.Log("start"+startTilt);
        startTilt = context.ReadValue<Vector3>();
        Debug.Log("start" + startTilt);
        // ���� ���ӵ��� ������ ��������
        Vector3 currentTilt = context.ReadValue<Vector3>();
        Vector3 tiltDelta = currentTilt - startTilt;

        // ��ǥ ȸ�� �� ����
        SetTargetRotation(tiltDelta);

        // ��ǥ ȸ�� ������ �ε巴�� ȸ��
        transform.rotation = targetRotation;
        ball.SetActive(true);
    }

    private void OnTiltPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("performed" + startTilt);
        // ���� ���ӵ��� ������ ��������
        Vector3 currentTilt = context.ReadValue<Vector3>();
        Vector3 tiltDelta = currentTilt - startTilt;

        // ��ǥ ȸ�� �� ����
        SetTargetRotation(tiltDelta);

        // ��ǥ ȸ�� ������ �ε巴�� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*0.6f);
        //cam.Render();
    }

    //private void Update()
    //{
    //    if(startTilt==new Vector3(0, 0, 0))
    //    {
    //        startTilt= Accelerometer.current.acceleration.ReadValue();
    //        return;
    //    }

    //    if (Accelerometer.current != null)
    //    {
    //         ���� ���ӵ��� ������ ��������
    //        Vector3 currentTilt = Accelerometer.current.acceleration.ReadValue();

    //         �ʱ� ������� ���̰� ���
    //        Vector2 tiltDelta = currentTilt - startTilt;

    //        x.text = "tilt X value : "+tiltDelta.x.ToString();
    //        y.text = "tile Z value : "+tiltDelta.y.ToString();

    //         ��ǥ ȸ�� �� ����
    //        SetTargetRotation(tiltDelta);

    //         ��ǥ ȸ�� ������ �ε巴�� ȸ��
    //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    //    }
    //}

    private void SetTargetRotation(Vector3 tiltDelta)
    {
        // X�� Z�࿡ ���� ��ǥ ȸ�� �� ���
        float tiltX = Mathf.Clamp(-tiltDelta.x * tiltMultiplier, -15f, 15f);
        float tiltZ = Mathf.Clamp(-tiltDelta.y * tiltMultiplier, -15f, 15f);

        // ��ǥ ȸ�� ���� ����
        targetRotation = Quaternion.Euler(tiltX, 0, tiltZ);
    }

    
}
