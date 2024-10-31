using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wiring : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private eColor color;
    private bool isSameColor;
    public bool IsSameColor { get => isSameColor; set => isSameColor = value; }

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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.pointerEnter.gameObject.name == "EndPoint" && eventData.pointerEnter.gameObject.TryGetComponent(out WiringPoint points))
        {
            if(this.color == points.GetWiringColor())
            {
                Debug.Log("���� ����");
                isSameColor = true;
            }
            else
            {
                Debug.Log("���� Ʋ��");
            }
        }
    }



}
