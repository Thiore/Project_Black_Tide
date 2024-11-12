using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, ITouchable
{
    //��ȣ�ۿ� ����
    [SerializeField] private int floorIndex; //���� �ִ� �� �ε���
    [SerializeField] private int objectIndex; // ��ȣ�ۿ� ������Ʈ �ε���
    private SaveManager saveManager;
    private bool isTouchable;
    private Animator ani;





    //���� ���� �� �ִ� �������� Ȯ�� (����� ���� �ִ� �� or �ݺ� ���� ����)
    private void CheckDoorState()
    {        
        ani = GetComponent<Animator>();

        isTouchable = SaveManager.Instance.PuzzleState(floorIndex, objectIndex);
    }

    //��ġ ���� ������ ��
    private void EnableDoorInteraction()
    {
        ani.SetBool("Open", true);
    }

    //��ġ �Ұ��� ������ ��
    private void DisableDoorInteraction()
    {
        //"����־�"��� ���� ��� ���
        DialogueManager.Instance.SetDialogue("Table_StoryB1", 1);

    }
    private void OnEnable()
    {
        //OnEnable���� �ڽ��� �� �� �ִ� �������� �ƴ��� Ȯ��
        CheckDoorState();
    }

    public void OnTouchStarted(Vector2 position)
    {
        CheckDoorState();
        if (isTouchable)
        {
            EnableDoorInteraction();
        }
        else
        {
            DisableDoorInteraction();
        }
    }

    public void OnTouchHold(Vector2 position)
    {

    }

    public void OnTouchEnd(Vector2 position)
    {

    }

}
