using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCamera : MonoBehaviour
{
    [Header("�̼� ����ġ")]
    [SerializeField] private GameObject[] mission = null; // �̼��� ��� �ִ� ���� ������Ʈ �迭

    [Header("������Ƽ")]
    [SerializeField] private float MissionSentenceOnDelay = 0; // �̼� ���� Ȱ��ȭ ���� �ð�

    private int delay_index = 0; // ���� Ȱ��ȭ�� �̼��� �ε���

    public void Delay() // �̼� ���� Ȱ��ȭ �Լ�
    {
        // ���� �̼��� �ڽ� ������Ʈ���� Ȱ��ȭ
        for (int i = 1; i < mission[delay_index].transform.childCount; i++)
        {
            mission[delay_index].transform.GetChild(i).gameObject.SetActive(true); // �̼� Ȱ��ȭ
        }
    }

    public void Mission(int index) // �̼� ī�޶� ON �Լ�
    {
        delay_index = index; // ���޹��� �ε����� ���� (������ �Լ��� ���)

        mission[delay_index].transform.GetChild(0).gameObject.SetActive(true); // �̼� ī�޶� Ȱ��ȭ

        Invoke("Delay", MissionSentenceOnDelay); // �̼� ���� Ȱ��ȭ ���� ȣ��
    }

    public void Close(int index) // �̼� ī�޶� OFF �Լ�
    {
        // ���� �̼��� ��� �ڽ� ������Ʈ���� ��Ȱ��ȭ
        for (int i = 0; i < mission[index].transform.childCount; i++)
        {
            mission[index].transform.GetChild(i).gameObject.SetActive(false); // �̼� ��Ȱ��ȭ
        }
    }
}