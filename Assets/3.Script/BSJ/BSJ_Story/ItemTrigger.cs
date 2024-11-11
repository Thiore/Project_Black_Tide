using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    // �ڽ��� �ε��� Item
    [SerializeField] private int itemExplanationIndex;

    private ReadInputData input;

    private void Start()
    {
        //ReadInputData ��������
        TryGetComponent(out input);
    }

    private void Update()
    {
        ItemText();
    }

    //��ȣ�ۿ� ���� ���
    private void ItemText()
    {
        if (gameObject.activeSelf && input.isTouch)
        {
            DialogueManager.Instance.SetDialogue("Table_ItemExplanation", itemExplanationIndex);
        }
    }
}
