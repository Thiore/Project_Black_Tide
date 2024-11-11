using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour, ITouchable
{
    // �ڽ��� �ε��� Item
    [SerializeField] private int itemExplanationIndex;
    [SerializeField] private int itemNum;

    //�κ��丮
    private GameObject mainPlayer;
    private GameObject canvas;
    private GameObject inventory;
    private UI_Inventory uiInventory;

    //��ȣ�ۿ� ���� ���
    private void ItemText()
    {
        if (gameObject.activeSelf)
        {
            DialogueManager.Instance.SetDialogue("Table_ItemExplanation", itemExplanationIndex);
        }
    }


    //Inventory ã��
    private void FindObjectUI()
    {
        mainPlayer = GameObject.FindGameObjectWithTag("Player");

        //Canvasã��
        Transform Canvas_ = mainPlayer.transform.GetChild(5);
        canvas = Canvas_.gameObject;

        //Inventory ã��
        if (canvas != null)
        {
            Transform Inventory = canvas.transform.GetChild(1);
            inventory = Inventory.gameObject;

            inventory.TryGetComponent(out uiInventory);
        }

    }

    //������ �ֱ�
    private void GetItem()
    {
        if (TryGetComponent(out Item item))
        {
            uiInventory.GetItemTouch(item);
        }
    }

    public void OnTouchStarted(Vector2 position)
    {
        
        ItemText();
        GetItem();
        //uiInventory.GetItemTouch();
    }

    public void OnTouchHold(Vector2 position)
    {
        
    }

    public void OnTouchEnd(Vector2 position)
    {
     
    }

    private void OnEnable()
    {
        FindObjectUI();
    }
}
