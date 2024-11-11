using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckWire : MonoBehaviour
{
    public StartPoint[] startArray;
    public event Action connectEvent;

    private void OnEnable()
    {
        foreach(var start in startArray)
        {
            start.CheckConnecting += CheckWireConnect;
        }
    }

    private void CheckWireConnect()
    {
        foreach(var start in startArray)
        {
            if (!start.isConnect)
            {
                Debug.Log("���� �����ƴ�");
                return;
            }
        }

        Debug.Log("���Ӽ���");
    }

}
