using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class ItemDatatest
{
    public string itemName;
    public Sprite itemIcon;
    public bool isCombine;
}

public class Slot : MonoBehaviour, IDropHandler
{
    public ItemDatatest currentItem; //���Կ� ��� ������
    public ItemDatatest[] possibleCombines; //���� ������ ������

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
            currentItem = draggableItem.GetComponent<ItemDatatest>();
            //�巡���� ������ ����
            Destroy(draggableItem.gameObject);
            return;
        }
        ItemDatatest draggedItem = draggableItem.GetComponent<ItemDatatest>();

        //����
        if (IsCombine(currentItem, draggedItem))
        {
            ItemDatatest comibnedItem = CombineItems(currentItem, draggedItem);

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
    private bool IsCombine(ItemDatatest item1, ItemDatatest item2)
    {
        return (item1.itemName == "1" && item2.itemName == "2") ||
                (item1.itemName == "2" && item2.itemName == "1");
    }

    private ItemDatatest CombineItems(ItemDatatest item1, ItemDatatest item2)
    {
        //���� ��� ������ ����
        return new ItemDatatest { itemName = "3", itemIcon = null };
    }

    //private void UpdaSlotUI()
    //{ 
    
    //}
}
