using UnityEngine;
using System;

public class GridSpin : MonoBehaviour
{
    public float rotationSpeed = 2f; // ȸ�� �ӵ�
    private Quaternion targetRotation; // ��ǥ ȸ����
    private ReadInputData readInput;
    private bool isRotating = false; // ȸ�� �� ���� Ȯ��
    public bool couldRotate = true;

    // ȸ�� �Ϸ� �� ������ �̺�Ʈ
    public event Action OnRotationComplete;

    private void Start()
    {
        readInput = GetComponent<ReadInputData>();
        if (readInput == null) Debug.Log("readinput null");
        targetRotation = transform.rotation; // ���� ȸ���� �ʱ� ��ǥ������ ����
    }

    private void Update()
    {
        // ȸ�� ���� �ƴ� ���� ��ġ ����
        if (!isRotating && readInput.isTouch && couldRotate)
        {
            Rotate();
            readInput.TouchTap();
        }

        // ��ǥ ȸ������ �ε巴�� ȸ��
        if (isRotating)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // ��ǥ ȸ���� �ٻ��ϸ� ��Ȯ�� ��ǥ ȸ������ ����
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
                isRotating = false;

                // ȸ�� �Ϸ� �� �̺�Ʈ ȣ��
                OnRotationComplete?.Invoke();
            }
        }
    }

    // 90�� ȸ�� ����
    private void Rotate()
    {
        targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0);
        isRotating = true; // ȸ�� ����
    }
}
