using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //��ȣ�ۿ� ����
    [SerializeField] private int floorIndex; //���� �ִ� �� �ε���
    [SerializeField] private int objectIndex; // ��ȣ�ۿ� ������Ʈ �ε���
    private SaveManager saveManager;
    private bool isTouchable;


    private Animator ani;
    private ReadInputData input;
    

    

    private void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();

        ani = GetComponent<Animator>();

        TryGetComponent(out input);

    }

    private void Update()
    {
        CheckDoorState();
    }

    //���� ���� �� �ִ� �������� Ȯ�� (����� ���� �ִ� �� or �ݺ� ���� ����)
    private void CheckDoorState()
    {
        //�ش� ���� ������Ʈ �ε����� �ش��ϴ� isInteracted ���� Ȯ��
        StateData.FloorState floor = saveManager.gameState.floors.Find(f => f.floorIndex == floorIndex);

        if (floor != null)
        {
            //�ش� ������Ʈ�� ���¸� Ȯ��
            StateData.InteractableObjectState doorState = floor.interactableObjects.Find(obj => obj.objectIndex == objectIndex);

            if (doorState != null)
            {
                //isInteracted ���� Ȯ�� ��, true��� ���� ��ġ ���� ���·�
                isTouchable = doorState.isInteracted;

                if (input.isTouch)
                {
                    if (isTouchable)
                    {
                        EnableDoorInteraction();
                        Debug.Log("����");
                    }
                    else if(!isTouchable)
                    {
                        DisableDoorInteraction();
                        Debug.Log("����");
                    }
                }
            }
        }
    }

    //��ġ ���� ������ ��
    private void EnableDoorInteraction()
    {
        if (isTouchable)
        {
            if (input.isTouch)
            {
                //ani.SetTrigger("Open");
                ani.SetBool("isOpen", true);
            }
        }
    }

    //��ġ �Ұ��� ������ ��
    private void DisableDoorInteraction()
    {
        //"����־�"��� ���� ��� ���
        if (!isTouchable)
        {
            if (input.isTouch)
            {
                DialogueManager.Instance.SetDialogue("Table_StoryB1", 0);
            }
        }
    }
}
