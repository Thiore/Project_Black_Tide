using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private _InputManager input;
    private Rigidbody rb;

    public float speed;

    Vector3 moveDir;

    private void Awake()
    {
        input = GetComponent<_InputManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �̵� ������ �Է� ������ ���� (���� �������� ��ȯ�ϱ� ��)
        moveDir = new Vector3(input.MoveInput.x, 0, input.MoveInput.y).normalized;
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            // �̵� ������ ���� ��ǥ��� ��ȯ
            Vector3 localMoveDir = transform.TransformDirection(moveDir);

            // ���� ��ǥ�� �������� �̵� ����
            rb.MovePosition(rb.position + localMoveDir * speed * Time.fixedDeltaTime);
        }
    }
}
