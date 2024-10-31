using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;

    private Vector3 tiltInput;

    [SerializeField] private Text x;
    [SerializeField] private Text y;
    [SerializeField] private Text z;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ���ӵ��� ���� ���� Ȯ��
        if (Accelerometer.current == null)
        {
            Debug.LogError("Accelerometer is not available on this device.");
        }
    }

    private void Update()
    {
        // �Է� �� �޾ƿ���
        tiltInput = ReadTiltInput();

        // ����׸� ���� UI ������Ʈ
        x.text = $"X : {tiltInput.x.ToString("F2")}";
        y.text = $"Y : {tiltInput.y.ToString("F2")}";
        z.text = $"Z : {tiltInput.z.ToString("F2")}";
    }

    private void FixedUpdate()
    {
        // ���⿡ ���� ���� Rigidbody�� �����Ͽ� �� �̵�
        Vector3 force = new Vector3(tiltInput.z, 0, -tiltInput.x) * speed;
        rb.AddForce(force);
    }

    private Vector3 ReadTiltInput()
    {
        // Accelerometer ������ �б�
        if (Accelerometer.current != null)
        {
            Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();
            return new Vector3(acceleration.x, 0, acceleration.y);
        }

        return Vector3.zero; // ���ӵ��谡 ���� ��� (�Ǵ� ���ķ����Ϳ��� ���� ���� ��)
    }
}
