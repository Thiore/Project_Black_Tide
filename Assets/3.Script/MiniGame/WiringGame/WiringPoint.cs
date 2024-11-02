using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiringPoint : MonoBehaviour
{
    private eColor color;
    private bool isSameColor = false;
    private bool isConnect;
    public bool IsConncet { get => isConnect; }

    public void SetboolConnect(bool isconnect)
    {
        isConnect = isconnect;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Wiring wiring))
        {
            wiring.SetBoolCheckColor(true);
            Debug.Log("���Ἲ��");
            isSameColor = wiring.WiringSameColorCheck(color);

            if (isSameColor)
            {
                Debug.Log("���� �� ����");
            }
            else
            {
                Debug.Log("�ٸ� �� ����");
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Wiring wiring))
        {
            wiring.SetBoolCheckColor(false);
            Debug.Log("��������");
        }
    }

    public eColor GetWiringColor()
    {
        return color;
    }

    public void SetWiringColor(int color)
    {
        this.color = (eColor)color;
    }

    
}
