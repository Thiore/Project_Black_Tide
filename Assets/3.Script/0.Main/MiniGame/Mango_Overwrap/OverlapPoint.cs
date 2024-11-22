 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using System;

public class OverlapPoint : MonoBehaviour, ITouchable
{
    public OverlapColor startColor;
    public Material lineMat;


    public LayerMask lineTouchLayer;

    public OverlapObj overlapObj;

    public List<OverlapObj> connectedObject;

    public event Action Check;


    private void Start()
    {
        overlapObj = GetComponent<OverlapObj>();
        overlapObj.line.material = lineMat;
    }




    public void OnTouchEnd(Vector2 position)
    {
        if (overlapObj.isConnected)
        {//������ �� ���
            foreach(var obj in connectedObject)
            {
                obj.isConnected = true;
            }
            Check?.Invoke();
        }
        else
        {
            foreach (var obj in connectedObject)
            {
                obj.isConnected = false;
                obj.line.enabled = false;
            }
            connectedObject.Clear();
        }
    }

    public void OnTouchHold(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        // ����ĳ��Ʈ�� ���� ���� ��ǥ ���
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, lineTouchLayer))
        {
            OverlapObj hitObject = hit.collider.GetComponent<OverlapObj>();

            string[] coordinate = hitObject.gameObject.name.Split(',');
            int x = 0;
            int.TryParse(coordinate[0] ,out x);
            int y;
            int.TryParse(coordinate[1], out y);

            if (overlapObj.isConnected)
            {
                return;
            }
            
            if(!hitObject.isPoint&& !hitObject.isConnected&&isAdjacent(x,y))
            {//����Ʈ�� �ƴϰ� ���ᵵ �ȵ� ��� (�� Ÿ��)
                if (connectedObject.Count >= 2)
                {
                    connectedObject[connectedObject.Count - 1].line.SetPosition(1, hitObject.linePos);
                }
                else
                {
                    overlapObj.line.SetPosition(1, hitObject.linePos);
                }
                hitObject.line.enabled = true;
                hitObject.line.material = lineMat;
                hitObject.line.SetPosition(0, hitObject.linePos);
                hitObject.line.SetPosition(1, hitObject.linePos);
                hitObject.connectColor = startColor;
                hitObject.isConnected = true;
                connectedObject.Add(hitObject);
            }
            else if (hitObject.isPoint && hitObject.connectColor.Equals(overlapObj.connectColor)&&!hitObject.Equals(overlapObj))
            {//���� ����Ʈ�� ��� (����/����)
                connectedObject[connectedObject.Count - 1].line.SetPosition(1, hitObject.linePos);
                overlapObj.isConnected = true;
                hitObject.isConnected = true;
                connectedObject.Add(hitObject);
                hitObject.GetComponent<OverlapPoint>().connectedObject = this.connectedObject;
                foreach(var obj in connectedObject)
                {
                    obj.isConnected = true;
                }

            }else if((hitObject.isPoint || hitObject.isConnected)&&!hitObject.connectColor.Equals(overlapObj.connectColor))
            {//�ٸ� ����Ʈ�̰ų� �ٸ� �÷��� ����� ���
                //�����Ǹ鼭 line disable
                foreach(var obj in connectedObject)
                {
                    obj.isConnected = false;
                    obj.line.enabled = false;
                }
                connectedObject.Clear();
                Handheld.Vibrate();

            }
            else if (hitObject.isConnected&&hitObject.connectColor.Equals(startColor)&&
                !connectedObject[connectedObject.Count-1].Equals(hitObject))
            {//���� ���� ����Ǿ��ִ� �Ϲ�Ÿ��
                int index = connectedObject.IndexOf(hitObject);

                for (int i = connectedObject.Count - 1; i >= index; i--)
                {
                    connectedObject[i].line.enabled = false;
                    connectedObject[i].isConnected = false;
                    connectedObject.RemoveAt(i);
                }
            }else if (hitObject.Equals(overlapObj))
            {
                foreach(var obj in connectedObject)
                {
                    obj.line.enabled = false;
                }
                connectedObject.Clear();
                overlapObj.line.enabled = true;
                overlapObj.line.SetPosition(0, overlapObj.linePos);
                overlapObj.line.SetPosition(1, overlapObj.linePos);
                connectedObject.Add(overlapObj);
            }

        }

    }

    public void OnTouchStarted(Vector2 position)
    {
        if (overlapObj.isConnected)
        {
            foreach (var obj in connectedObject)
            {
                if (obj.GetComponent<OverlapPoint>() != null&&!obj.Equals(overlapObj)) obj.GetComponent<OverlapPoint>().connectedObject.Clear();

                obj.isConnected = false;
                obj.line.enabled = false;
            }
            connectedObject.Clear();
            overlapObj.line.enabled = true;
            overlapObj.line.SetPosition(0, overlapObj.linePos);
            overlapObj.line.SetPosition(1, overlapObj.linePos);
            connectedObject.Add(overlapObj);
        }
        else
        {
        Debug.Log($"{gameObject.name} ��ġ��");
        overlapObj.line.enabled = true;
        overlapObj.line.SetPosition(0, overlapObj.linePos);
        overlapObj.line.SetPosition(1, overlapObj.linePos);
        connectedObject.Add(overlapObj);
        }

    }
    private bool isAdjacent(int x, int y)
    {
        // ������Ʈ�� �̸����� ��ǥ�� ����
        string[] coordinate = connectedObject[connectedObject.Count-1].name.Split(',');
        int myX = int.Parse(coordinate[0]);
        int myY = int.Parse(coordinate[1]);

        // ���� ���θ� �Ǵ��ϴ� ����
        bool isAdjacentX = (myX == x && (myY == y + 1 || myY == y - 1)); // y ��ǥ�� ��1 ����
        bool isAdjacentY = (myY == y && (myX == x + 1 || myX == x - 1)); // x ��ǥ�� ��1 ����

        return isAdjacentX || isAdjacentY; // �� �� �ϳ��� �����̶� �����ϸ� true ��ȯ
    }
}

public enum OverlapColor
{
    Blue,
    Gray,
    Pink,
    Red,
    Mint,
    Orange,
    Yellow,
    Purple,
    Null
}
