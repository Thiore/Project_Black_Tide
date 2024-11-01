using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockGame : MonoBehaviour
{
    private int[] correctNumber = { 1, 3, 0, 4 }; //���� ��ȣ
    private int[] currentNumber = { 0, 0, 0, 0 }; //���� ��ȣ

    //�� ��ȣ �� ������Ʈ (4��)
    public Transform[] numberWheels;

    public Animator ani;

    //ȸ�� ���� ����
    private float rotationAngle = 36f;

    //ȸ�� �ӵ�
    private float rotationSpeed = 5f;

    //�� ���� ��ǥ ȸ�� ����
    private Quaternion[] targetRotations;

    public Camera mainCamera;


    private void Start()
    {
        Debug.Log("�̰� ������?");
        ani.GetComponent<Animator>();

        //��ȣ ����
        ResetLock();

        //�ʱ� ��ǥ ȸ�� ����
        targetRotations = new Quaternion[numberWheels.Length];
        for (int i = 0; i < numberWheels.Length; i++)
        {
            targetRotations[i] = numberWheels[i].localRotation;
        }
    }

    
    //��ȣ ����
    public void ResetLock()
    {
        for (int i = 0; i < currentNumber.Length; i++)
        {
            currentNumber[i] = 0;
            numberWheels[i].localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    

    //Ư�� ��ȣ ���� ���������� ȸ��(+36��)
    public void RotateWheelRight(int wheelIndext)
    {
        currentNumber[wheelIndext] = (currentNumber[wheelIndext] + 1) % 10;

        //�� ��ȣ ���� �ش� ���ڿ� �°� ȸ��
        float newRotation = currentNumber[wheelIndext] * rotationAngle;
        targetRotations[wheelIndext] = Quaternion.Euler(0, newRotation, 0);

        //����Ȯ��
        CheckNumber();
    }

    //Ư�� ��ȣ ���� �������� ȸ�� (-36��)
    public void RotateWheelLeft(int wheelIndex)
    {
        //���ڸ� ���ҽ�Ű��, 0���� ������ 9�� ����
        currentNumber[wheelIndex] = (currentNumber[wheelIndex] - 1 + 10) % 10;

        //�� ��ȣ ���� �ش� ���ڿ� �°� ȸ��
        float newRotation = currentNumber[wheelIndex] * rotationAngle;
        targetRotations[wheelIndex] = Quaternion.Euler(0, newRotation, 0);

        //���� Ȯ��
        CheckNumber();
    }

    //���� ��ȣ�� ���� ��ȣ ��
    private void CheckNumber()
    {
        for (int i = 0; i < correctNumber.Length; i++)
        {
            if (currentNumber[i] != correctNumber[i])
                return; //�ϳ��� Ʋ���� ��ȯ
        }

        //��ȣ�� ������ �ڹ��� ������ �ִϸ��̼� ����
        LockOpenAnimation();
    }

    private void LockOpenAnimation()
    {
        ani.SetTrigger("Open");
        Debug.Log("����");
    }

    private void Update()
    {
        for (int i = 0; i < numberWheels.Length; i++)
        {
            //��ǥ ȸ�� �������� �ε巴��
            numberWheels[i].localRotation = Quaternion.Lerp
                (numberWheels[i].localRotation,
                targetRotations[i],
                Time.deltaTime * rotationSpeed);
                
        }
    }
}
