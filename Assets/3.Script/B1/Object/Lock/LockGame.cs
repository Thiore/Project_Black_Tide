using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockGame : MonoBehaviour
{
    //��ȣ�ۿ� ����
    [SerializeField] private int floorIndex; //������Ʈ�� ���� ��
    [SerializeField] private int objectIndex; //������Ʈ ������ �ε���
    private SaveManager saveManager; //���°���

    private int[] correctNumber = { 1, 3, 0, 4 }; //���� ��ȣ
    private int[] currentNumber = { 0, 0, 0, 0 }; //���� ��ȣ

    //ȸ������ �� �ڷ�ƾ
    private Coroutine[] rotation_co;

    //�� ��ȣ �� ������Ʈ (4��)
    public Transform[] numberWheels;

    public Animator ani;

    //ȸ�� ���� ����
    private float rotationAngle = -36f;

    //ȸ�� �ӵ�
    private float rotationSpeed = 5f;

    //�� ���� ��ǥ ȸ�� ����
    private Quaternion[] targetRotations;


    
    public GameObject canvas;
    public bool isAnswer;


    private void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();

        ani.GetComponent<Animator>();

        //�ʱ� ��ǥ ȸ�� ����
        targetRotations = new Quaternion[numberWheels.Length];
        for (int i = 0; i < numberWheels.Length; i++)
        {
            targetRotations[i] = numberWheels[i].localRotation;
        }

        //ȸ���ڷ�ƾ �ʱ�ȭ
        rotation_co = new Coroutine[4];
        for(int i = 0; i < 4;i++)
        {
            rotation_co[i] = null;
        }

        //��ȣ ����
        ResetLock();
    }
    //private void Update()
    //{
    //    //GameSetting();
    //    for (int i = 0; i < numberWheels.Length; i++)
    //    {
    //        //��ǥ ȸ�� �������� �ε巴��
    //        numberWheels[i].localRotation = Quaternion.Lerp
    //            (numberWheels[i].localRotation,
    //            targetRotations[i],
    //            Time.deltaTime * rotationSpeed);

    //    }
    //}

    //��ȣ ����
    public void ResetLock()
    {
        for (int i = 0; i < currentNumber.Length; i++)
        {
            if(rotation_co[i] != null)
            {
                StopCoroutine(rotation_co[i]);
                rotation_co[i] = null;
            }
            
            currentNumber[i] = 0;
            numberWheels[i].localRotation = Quaternion.Euler(0f, 0f, -180f);
        }
    }

    

    //Ư�� ��ȣ ���� ���������� ȸ��(+36��)
    public void RotateWheelRight(int wheelIndex)
    {
        if(rotation_co[wheelIndex] == null && !isAnswer)
        {
            currentNumber[wheelIndex] = (currentNumber[wheelIndex] + 1) % 10;

            //�� ��ȣ ���� �ش� ���ڿ� �°� ȸ��
            float newRotation = currentNumber[wheelIndex] * rotationAngle;
            targetRotations[wheelIndex] = Quaternion.Euler(0, newRotation, -180);
            rotation_co[wheelIndex] = StartCoroutine(RotateWheel(wheelIndex));
        }
    }

    //Ư�� ��ȣ ���� �������� ȸ�� (-36��)
    public void RotateWheelLeft(int wheelIndex)
    {
        if (rotation_co[wheelIndex] == null&&!isAnswer)
        {
            //���ڸ� ���ҽ�Ű��, 0���� ������ 9�� ����
            currentNumber[wheelIndex] = (currentNumber[wheelIndex] - 1 + 10) % 10;

            //�� ��ȣ ���� �ش� ���ڿ� �°� ȸ��
            float newRotation = currentNumber[wheelIndex] * rotationAngle;
            targetRotations[wheelIndex] = Quaternion.Euler(0, newRotation, -180);
        }
    }

    /// <summary>
    /// �ڷ�ƾ ȸ���� ���� �����Դϴ�.
    /// </summary>
    /// <param name="wheelIndex">���õ� ���� index��</param>
    /// <returns></returns>
    private IEnumerator RotateWheel(int wheelIndex)
    {
        float rotationTime = 0f;

        while(rotationTime<1f)
        {
            //��ǥ ȸ�� �������� �ε巴��
            numberWheels[wheelIndex].localRotation =
                Quaternion.Slerp(numberWheels[wheelIndex].localRotation,
                                 targetRotations[wheelIndex],
                                 rotationTime / rotationSpeed);
            yield return null;
        }
        //���� Ȯ��
        CheckNumber();
        rotation_co = null;
        yield break;

    }

    //���� ��ȣ�� ���� ��ȣ ��
    private void CheckNumber()
    {
        for (int i = 0; i < correctNumber.Length; i++)
        {
            if (currentNumber[i] != correctNumber[i])
                return; //�ϳ��� Ʋ���� ��ȯ
        }

        //��ȣ�� ������ ����(true) ����
        saveManager.UpdateObjectState(floorIndex, objectIndex, true);

        isAnswer = true;
        canvas.gameObject.SetActive(false);
        //��ȣ�� ������ �ڹ��� ������ �ִϸ��̼� ����
        LockOpenAnimation();
    }

    private void LockOpenAnimation()
    {
        //EndGame();
        ani.SetTrigger("Open");
        Debug.Log("����");
    }

    //private void GameSetting()
    //{
    //    if (input.isTouch)
    //    {
    //        camera1.gameObject.SetActive(true);
    //        canvas.gameObject.SetActive(true);
    //    }
    //}

    //private void EndGame()
    //{
    //    camera1.gameObject.SetActive(false);
    //    canvas.gameObject.SetActive(false);
    //    camera2.gameObject.SetActive(true);
    //}
}
