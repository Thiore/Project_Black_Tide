using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapCheck : MonoBehaviour,ITouchable
{
    public List<OverlapPoint> pointList;

    private void OnEnable()
    {
        foreach(var point in pointList)
        {
            point.Check += CheckComplete;
        }
    }

    public void ResetBtnClick()
    {
        foreach(var point in pointList)
        {
            if (point.connectedObject.Count == 0) continue;
            
            foreach (var obj in point.connectedObject)
            {
                obj.isConnected = false;
                obj.line.enabled = false;
            }
            point.connectedObject.Clear();
        }
    }
    public bool CheckComplete()
    {
        foreach(var point in pointList)
        {
            if (!point.GetComponent<OverlapObj>().isConnected)
            {
                Debug.Log("���� ���� �ȳ���");
                return false;
            }
        }
        Debug.Log("���ӳ���");
        return true;
    }

    public void OnTouchStarted(Vector2 position)
    {
        ResetBtnClick();
    }

    public void OnTouchHold(Vector2 position)
    {
    }

    public void OnTouchEnd(Vector2 position)
    {
    }
}