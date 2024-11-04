using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wiring : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private eColor color;
    private bool isCheckColor;
    public bool IsCheckColor { get => isCheckColor; }
    public void SetBoolCheckColor(bool checkcolor)
    {
        isCheckColor = checkcolor;
    }

    private ReadInputData input;

    private void Start()
    {
        TryGetComponent(out input);
    }

    private void Update()
    {
        if (input.isTouch)
        {
            DragConnectWiring();
        }
    }

    public bool WiringSameColorCheck(eColor color)
    {
        if(this.color == color)
        {
            isCheckColor = true;
        }
        else
        {
            isCheckColor = false;
        }

        return isCheckColor;
    }

    public void SetColor(eColor color)
    {
        this.color = color;
    }

    public eColor GetWiringColor()
    {
        return color;
    }

    public void SetWiringColor(int color)
    {
        this.color = (eColor)color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(color);
        // ��ġ�� �ű�ٱ⺸�ٴ� ���̴ϱ�, ��ġ�� �������� ���� 
        // onenter �Ǹ� ��ġ�� false �ϴ� ������ 
        // ���η�����
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.pointerEnter.gameObject.name == "EndPoint" && eventData.pointerEnter.gameObject.TryGetComponent(out WiringPoint points))
        {
            if(this.color == points.GetWiringColor())
            {
                Debug.Log("���� ����");
                isCheckColor = true;
            }
            else
            {
                Debug.Log("���� Ʋ��");
            }
        }
    }


    // 
    public void DragConnectWiring()
    {
        //if (inputdata.isTouch)
        //{
        //    //��ġ�� ���°��� 
        //    //inputdata.value �հ��� ����ٴϴ� ���   
        //}

        Debug.Log("ȣ��");

    }
}
