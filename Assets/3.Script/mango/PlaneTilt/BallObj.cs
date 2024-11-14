using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObj : MonoBehaviour
{
    public WallColor recentPassWall;
    public Vector3 startPos;
    private bool isStart;
    private void Awake()
    {
        isStart = false;
    }

    private void OnTriggerExit(Collider wall)
    {
        if (wall.GetComponent<MazeWall>() != null)
        {
            if (recentPassWall.Equals(wall.GetComponent<MazeWall>().color)&&isStart)
            {
                if (wall.GetComponent<MazeWall>().GetExitDirection(this.transform))
                {
                    recentPassWall = wall.GetComponent<MazeWall>().color;
                }
                else
                {
                    transform.position = startPos;
                    isStart = false;
                }
            }
            else
            {
                isStart = true;
                recentPassWall = wall.GetComponent<MazeWall>().color;
            }
        }
    }
}

public enum WallColor
{
    Blue,
    Red,
    Green
}

public class MazeWall :MonoBehaviour 
{
    public WallColor color;
    public WallNode enterDirection;
    public BallObj ball;

    


    private void OnTriggerEnter(Collider Ball)
    {
        Vector3 direction = Ball.transform.position - transform.position; // ��� ��ġ ���

        // ���� ����
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z)) // x�� �������� ���� �Ǵ�
        {
            if (direction.x > 0)
                enterDirection = WallNode.Right;
            else
                enterDirection = WallNode.Left;
        }
        else // z�� �������� ���� �Ǵ�
        {
            if (direction.z > 0)
                enterDirection = WallNode.Top;
            else
                enterDirection = WallNode.Bottom;
        }

        Debug.Log("���� ����: " + enterDirection);
    }

    public bool GetExitDirection(Transform ball)
    {
        Vector3 direction = ball.position - transform.position;
        WallNode exitDirection;
        if (enterDirection==WallNode.Right||enterDirection==WallNode.Left)
        {
            if (direction.x > 0)
                exitDirection = WallNode.Right;
            else
                exitDirection = WallNode.Left;
        }
        else
        {
            if (direction.z > 0)
                exitDirection = WallNode.Top;
            else
                exitDirection = WallNode.Bottom;
        }

        Debug.Log("���� ����: " + exitDirection);

        // ���� ����� ���� ���� ��
        if (exitDirection == enterDirection)
        {
            Debug.Log("���� �������� ����");
            return true;
        }
        else
        {
            Debug.Log("�ٸ� �������� ����");
            return false;
        }

    
    }
}

public enum WallNode
{
    None,
    Top,
    Bottom,
    Left,
    Right
}