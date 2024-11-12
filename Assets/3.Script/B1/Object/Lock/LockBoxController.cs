using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBoxController : MonoBehaviour
{
    [SerializeField] private int floorIndex; //������Ʈ�� ���� ��
    [SerializeField] private int objectIndex; // ��ȣ�ۿ� ������Ʈ �ε���
    private Animator ani;
    private ReadInputData input;
    private bool isAnswer; //��ȣ�ۿ� ������Ʈ ���俩��


    private void Start()
    {
        ani = GetComponent<Animator>();
        TryGetComponent(out input);
    }

    private void Update()
    {
        CheckBoxState();
    }

    //��ȣ�ۿ� ������Ʈ ���¿� ���� ���� ���� ����
    private void CheckBoxState()
    {
        //�ش� ���� ������Ʈ �ε����� �ش��ϴ� isInteracted ���� Ȯ��
        StateData.FloorState floor = SaveManager.Instance.gameState.floors.Find(f => f.floorIndex == floorIndex);

        if (floor != null)
        {
            //�ش� ������Ʈ�� ���¸� Ȯ��
            StateData.InteractableObjectState boxState = floor.interactableObjects.Find(obj => obj.objectIndex == objectIndex);

            if (boxState != null)
            {
                isAnswer = boxState.isInteracted;
                if (isAnswer)
                {
                    ani.SetTrigger("Open");
                }
            }
        }
    }
}
