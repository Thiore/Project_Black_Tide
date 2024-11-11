using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualController : MonoBehaviour,ITouchable
{
    private bool isvirtualOff = true;

    // �ó׸ӽ� ���߾� ī�޶�
    [SerializeField] private GameObject virtualCam;

    public void OnTouchEnd(Vector2 position)
    {
        
    }

    public void OnTouchHold(Vector2 position)
    {
        
    }

    public void OnTouchStarted(Vector2 position)
    {
        ObjTouch();
    }

    private void ObjTouch()
    {
        if (isvirtualOff)
        {
            virtualCam.gameObject.SetActive(true);
            isvirtualOff = false;
        }
        else if (virtualCam.activeSelf && !isvirtualOff)
        {
            virtualCam.gameObject.SetActive(false);
            isvirtualOff = true;
        }
    }
}
