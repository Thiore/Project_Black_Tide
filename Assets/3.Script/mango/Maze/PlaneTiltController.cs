using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlaneTiltController : MonoBehaviour
{
    private Vector3 startTilt=new Vector3(0,0,0); // �ʱ� ���� ��
    public float tiltMultiplier = 90f; // ���� ������ �����ϴ� ����
    public float smoothSpeed = 2f; // ȸ���� �ε巯���� �����ϴ� �ӵ�

    [SerializeField] private Text x;
    [SerializeField] private Text y;
    [SerializeField] private Text z;

    private Quaternion targetRotation; // ��ǥ ȸ�� ��

    private void Start()
    {
        if (Accelerometer.current != null)
        {
            // ���� ������ ���� ���� �����Ͽ� ���������� ���
            startTilt = Accelerometer.current.acceleration.ReadValue();
        }
        else
        {
            Debug.LogWarning("Accelerometer not available on this device. Please run on an actual device.");
        }
    }

    private void Update()
    {
        //if(startTilt==new Vector3(0, 0, 0))
        //{
        //    startTilt= Accelerometer.current.acceleration.ReadValue();
        //    return;
        //}

        if (Accelerometer.current != null)
        {
            // ���� ���ӵ��� ������ ��������
            Vector3 currentTilt = Accelerometer.current.acceleration.ReadValue();

            // �ʱ� ������� ���̰� ���
            Vector3 tiltDelta = currentTilt - startTilt;

            x.text = "tilt X value : "+tiltDelta.x.ToString();
            y.text = "tile Z value : "+tiltDelta.y.ToString();

            // ��ǥ ȸ�� �� ����
            SetTargetRotation(tiltDelta);

            // ��ǥ ȸ�� ������ �ε巴�� ȸ��
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
        }
    }

    private void SetTargetRotation(Vector3 tiltDelta)
    {
        // X�� Z�࿡ ���� ��ǥ ȸ�� �� ���
        float tiltX = Mathf.Clamp(-tiltDelta.x * tiltMultiplier, -5f, 5f);
        float tiltZ = Mathf.Clamp(-tiltDelta.y * tiltMultiplier, -5f, 5f);

        // ��ǥ ȸ�� ���� ����
        targetRotation = Quaternion.Euler(tiltX, 0, tiltZ);
    }
}
