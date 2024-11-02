using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBox : MonoBehaviour
{
    private ReadInputData input;
    public GameObject[] cinemachine;

    private void Start()
    {
        TryGetComponent(out input);
        if (input == null)
        {
            Debug.LogError("ReadInputData ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        Debug.Log(input);
    }

    private void Update()
    {
        if (input.isTouch)
        {
            Debug.Log("���� �����");
            cinemachine[0].gameObject.SetActive(true);
        }
    }

    


}
