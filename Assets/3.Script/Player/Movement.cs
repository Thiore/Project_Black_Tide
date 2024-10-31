using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private InputManager input; // input���� ������ �Ŵ���
    
    [SerializeField] private Camera playerCamera; //�÷��̾ ������ ������ ī�޶�

    
    [SerializeField] private float speed; //�÷��̾��� �ӵ�

    
    Vector3 moveDir; //�÷��̾ �̵��� ���⺤��

    private void Awake()
    {
        input = InputManager.Instance;
    }

    private void Update()
    {
        //�÷��̾������Ʈ�� �ٶ� ���� ����
        //Vector3 cameraRot = new Vector3(0, playerCamera.transform.localEulerAngles.y, 0);
        //transform.eulerAngles = cameraRot;
        // �̵� ������ �ʱ� ��ġ ��ǥ�� ���� �Է� ��ǥ�� ���
        Vector2 joystickInput = input.moveData.value - input.moveData.startValue;
        //���� ��ǥ�� �̵��ؾ��� ���⺤�� ����
        moveDir = new Vector3(joystickInput.x, 0, joystickInput.y).normalized;

        
    }

    private void FixedUpdate()
    {
        //�̸� ���س��� ���⺤�ͷ� �÷��̾� �̵�
        transform.Translate(moveDir * speed * Time.fixedDeltaTime);
        
    }
}
