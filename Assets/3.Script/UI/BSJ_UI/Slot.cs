using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemIcon;
    public bool isCombine;
}

public class Slot : MonoBehaviour, IDropHandler
{
    public Item currentItem; //���Կ� ��� ������
    public Item[] possibleCombines; //���� ������ ������

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (draggableItem != null)
        {
            //���� ���Կ� ��� �����۰� ����
            TryCombineItem(draggableItem);
        }
    }
    
    private void TryCombineItem(DraggableItem draggableItem)
    {
        //���� ���Կ� �������� ������ �巡���� �������� �߰�
        if (currentItem == null)
        {
            //���Կ� ������ �߰�
            currentItem = draggableItem.GetComponent<Item>();
            //�巡���� ������ ����
            Destroy(draggableItem.gameObject);
            return;
        }
        Item draggedItem = draggableItem.GetComponent<Item>();

        //����
        if (IsCombine(currentItem, draggedItem))
        {
            Item comibnedItem = CombineItems(currentItem, draggedItem);

            if (comibnedItem != null)
            {
                //���� ����� ������ �������� ������Ʈ
                currentItem = comibnedItem;


                //UI ������Ʈ


                //�巡���� ������ ����
                Destroy(draggableItem.gameObject);
               
            }
        }
    }
    private bool IsCombine(Item item1, Item item2)
    {
        return (item1.itemName == "1" && item2.itemName == "2") ||
                (item1.itemName == "2" && item2.itemName == "1");
    }

    private Item CombineItems(Item item1, Item item2)
    {
        //���� ��� ������ ����
        return new Item { itemName = "3", itemIcon = null };
    }

    //private void UpdaSlotUI()
    //{ 
    
    //}
}
