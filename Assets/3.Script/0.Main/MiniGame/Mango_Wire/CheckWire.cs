using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckWire : PlayOBJ
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
                Debug.Log("아직 성공아님");
                return;
            }
        }

        Debug.Log("게임성공");
        SaveManager.Instance.UpdateObjectState(floorIndex, objectIndex[0], true);
        puzzle.OffInteraction();
        Debug.Log("나와라");
    }

}
