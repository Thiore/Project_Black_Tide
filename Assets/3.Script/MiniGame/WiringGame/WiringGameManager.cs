using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�÷� ������ ���� �� ��ġ���� �����Ҳ��� �����ּ��� 
public enum eColor
{
   Green = 0,
   Orange,
   Red,
   Yellow
}

public class WiringGameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] startPoints;
    [SerializeField] private GameObject[] endPoints;
    [SerializeField] private GameObject[] wirings;

    private WiringPoint[] endWiringPoints;

    private void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        InitPoints();

    }


    //�輱 ���� �� ��ġ ����
    private void InitPoints()
    {
        endWiringPoints = new WiringPoint[4];

        //�� ��������/������/���� ���� �� boolean �ʱ�ȭ 
        for (int i = 0; i < startPoints.Length; i++) 
        {
            if(startPoints[i].TryGetComponent(out WiringPoint startwiringpoint))
            {
                startwiringpoint.SetWiringColor(i);
                startwiringpoint.SetboolConnect(true);
            }

            if (endPoints[i].TryGetComponent(out WiringPoint endwiringpoint))
            {
                endwiringpoint.SetWiringColor(i);
                endwiringpoint.SetboolConnect(false);
                endWiringPoints[i] = endwiringpoint;
                
            }

            if (wirings[i].TryGetComponent(out Wiring wiring))
            {
                wiring.SetWiringColor(i);
            }
        }

        int shufflecount = Random.Range(1, 10);
        for(int i = 0; i < shufflecount; i++)
        {
            int index = Random.Range(0, 4);
            int nextindex = Random.Range(0, 4);

            if(index == nextindex)
            {
                nextindex = Random.Range(0, 4);
            }

            Vector3 emptypos = startPoints[index].transform.position;
            startPoints[index].transform.position = startPoints[nextindex].transform.position;
            startPoints[nextindex].transform.position = emptypos;
        }

        for (int i = 0; i < shufflecount; i++)
        {
            int index = Random.Range(0, 4);
            int nextindex = Random.Range(0, 4);

            if (index == nextindex)
            {
                nextindex = Random.Range(0, 4);
            }

            Vector3 emptypos = endPoints[index].transform.position;
            endPoints[index].transform.position = endPoints[nextindex].transform.position;
            endPoints[nextindex].transform.position = emptypos;
        }

        // �� ���� �輱 ��ġ ����
        for (int i = 0; i < startPoints.Length; i++)
        {
            Vector2 wiringspos = new Vector2(startPoints[i].transform.localPosition.x + 66f, startPoints[i].transform.localPosition.y);
            wirings[i].transform.position = wiringspos;
        }


    }


    private void CheckWiringBool()
    {
        int check = 0;
        for(int i = 0; i < endWiringPoints.Length; i++)
        {
            if (endWiringPoints[i].IsConncet)
            {
                check++;
            }
        }

        if(check >= 4)
        {
            // �̰� ���� �� 
        }
        else
        {
            check = 0;
        }
    }
}

