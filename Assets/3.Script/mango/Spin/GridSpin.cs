using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpin : MonoBehaviour
{
    public float rotationSpeed = 2f; // ȸ�� �ӵ�
    private Quaternion targetRotation; // ��ǥ ȸ����

    private void Start()
    {
        targetRotation = transform.rotation; // ���� ȸ���� �ʱ� ��ǥ������ ����
    }

    private void Update()
    {

    }

    // 90�� ȸ�� ����
    private void Rotate()
    {
        targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0);
    }
}
