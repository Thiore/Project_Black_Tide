 using System;
 using UnityEngine;

public class Cylinder : MonoBehaviour
{
    private int minValue = 0;
    private int maxValue = 180;
    public float rotateSpeed;
    public float correctValue;

    public event Action AfterRotate; // ȸ�� �Ϸ� �� ������ �̺�Ʈ

    private Quaternion targetRotation; // ��ǥ ȸ����
    private bool isRotating = false; // ȸ�� �� ���� Ȯ��

    private Quaternion SetRotateValue(float rotateValue)
    {
        float targetRotationZ;

        // ��ǥ ȸ���� ���� (minValue�� maxValue ���̷� ����)
        if (transform.eulerAngles.z + rotateValue > maxValue)
        {
            targetRotationZ = maxValue;
        }
        else if (transform.eulerAngles.z + rotateValue < minValue)
        {
            targetRotationZ = minValue;
        }
        else
        {
            targetRotationZ = transform.eulerAngles.z + rotateValue;
        }

        return Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetRotationZ);
    }

    public void Rotate(float rotateValue)
    {
        targetRotation = SetRotateValue(rotateValue); // ��ǥ ȸ���� ����
        isRotating = true; // ȸ�� ����
    }

    private void Update()
    {
        if (isRotating)
        {
            // ��ǥ ȸ������ �ε巴�� ȸ��
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

            // ��ǥ ȸ���� �ٻ��ϸ� ȸ�� ����
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
                AfterRotate?.Invoke(); // ȸ�� �Ϸ� �̺�Ʈ ȣ��
            }
        }
    }
}
