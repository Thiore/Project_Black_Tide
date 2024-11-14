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

    

    private void OnEnable()
    {

        if (Accelerometer.current != null)
        {
            startTilt = Accelerometer.current.acceleration.ReadValue();
            Debug.Log("���̤��̤��ƾƾƾ�");
        }
        else
        {

            Debug.LogWarning("Accelerometer not available on this device. Please run on an actual device.");
        }
    }

    private void Update()
    {
        if (Accelerometer.current.enabled)
        {
            // ���� ���ӵ��� ������ ��������
            Vector3 currentTilt = Accelerometer.current.acceleration.ReadValue();


            // �ʱ� ������� ���̰� ���
            Vector3 tiltDelta = currentTilt - startTilt;

            x.text = "tilt X value : "+tiltDelta.x.ToString();
            y.text = "tile Z value : "+tiltDelta.y.ToString();

            Debug.Log($"Accelerometer.current.acceleration {Accelerometer.current.acceleration.ReadValue().x} , {Accelerometer.current.acceleration.ReadValue().y}");

            // ��ǥ ȸ�� �� ����
            SetTargetRotation(tiltDelta);

            // ��ǥ ȸ�� ������ �ε巴�� ȸ��
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
        }
        else
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
    }

    private void SetTargetRotation(Vector3 tiltDelta)
    {
        // X�� Z�࿡ ���� ��ǥ ȸ�� �� ���
        float tiltX = Mathf.Clamp(-tiltDelta.x * tiltMultiplier, -45f, 45f);
        float tiltZ = Mathf.Clamp(-tiltDelta.y * tiltMultiplier, -45f, 45f);

        // ��ǥ ȸ�� ���� ����
        targetRotation = Quaternion.Euler(tiltX, 0, tiltZ);
    }

    public void GetAccelerometer()
    {
        if (Accelerometer.current == null) Debug.Log("���Ӱ� ����������");
        else
        {
            Debug.Log("���Ӱ� ����");
            Debug.Log($"accelermeter Ȱ��ȭ ���� : {Accelerometer.current.enabled}");
            Debug.Log($"atitude sensor Ȱ��ȭ ���� : { AttitudeSensor.current.enabled}");
            
            startTilt = Accelerometer.current.acceleration.value;

        }
    }
}
