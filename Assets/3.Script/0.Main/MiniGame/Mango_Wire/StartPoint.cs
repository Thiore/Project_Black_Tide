 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using System;
public enum WireColor
{
    Yellow,
    Green,
    Red,
    Orange
}
public interface ISetColor
{
    public void setColor(WireColor color);
    public void setMaterial();
}

public class StartPoint : MonoBehaviour, ITouchable,ISetColor
{
    public WireColor color;
    [SerializeField] private GameObject wire;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform lineStartPoint;
    public float lineWidth;
    public LayerMask lineTouchLayer;


    public Material redMat;
    public Material yellowMat;
    public Material greenMat;
    public Material orangeMat;

    [SerializeField]private Renderer render;


    private Vector3 worldPosition; // ��ġ ��ġ�� ���� ��ǥ�� ������ ����
    private bool isTouching; // ��ġ ������ ���� Ȯ��
    private bool isConnected;
    private GameObject connectedObject;

    public bool isConnect;

    public event Action CheckConnecting;

    private void OnEnable()
    {
        line.startWidth=lineWidth;
        line.endWidth = lineWidth;
        line.enabled = false;
    }

    public void OnTouchEnd(Vector2 position)
    {

        if (this.isConnected.Equals(true))
        {
            connectedObject.GetComponent<EndPoint>().isconnected = true;
            isTouching = false;
            isConnect = true;
            CheckConnecting?.Invoke();
        }
        else
        {
            wire.SetActive(true);
            line.enabled = false;
            isTouching = false;
            isConnect = false;
        }

       
    }

    public void OnTouchHold(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        // ����ĳ��Ʈ�� ���� ���� ��ǥ ���
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, lineTouchLayer))
        {
            worldPosition = hit.point; // �浹�� ������ ��ǥ�� ���

            if(hit.collider.GetComponent<EndPoint>()!=null&&hit.collider.GetComponent<EndPoint>().color.Equals(color))
            {
                line.SetPosition(1, hit.collider.GetComponent<EndPoint>().lineEndPostion.position);
                this.isConnected = true;
                connectedObject = hit.collider.gameObject;
            }
            else
            {
                line.SetPosition(1,worldPosition);
                this.isConnected = false;
                connectedObject = null;
            }

        }

    }

    public void OnTouchStarted(Vector2 position)
    {
        wire.SetActive(false);
        line.enabled = true;
        line.SetPosition(0, lineStartPoint.position);
        line.SetPosition(1, lineStartPoint.position);
        isTouching = true;
    }

    public void setColor(WireColor color)
    {
        this.color = color;
    }

    public void setMaterial()
    {
        if (render == null) Debug.Log("render�� null");
        switch (color)
        {
            case WireColor.Yellow:
                render.material = yellowMat;
                break;
            case WireColor.Green:
                render.material = greenMat;
                break;
            case WireColor.Red:
                render.material = redMat;
                break;
            case WireColor.Orange:
                render.material = orangeMat;
                break;
        }
    }
}
