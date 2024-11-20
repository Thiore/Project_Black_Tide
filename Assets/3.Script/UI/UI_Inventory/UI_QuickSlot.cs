using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_QuickSlot : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private int id = -1;
    public int SlotID { get => id; }
    [SerializeField] private Item item;
    [SerializeField] private Image image;
    private bool isdragging = false;
    public void FragIsDrag()
    {
        isdragging = !isdragging;
    }

    public void SetinvenByID(Item item)
    {
        id = item.id;
        this.item = item;
        image.sprite = item.sprite;
        image.enabled = true;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("������");
        isdragging = true;
        UI_InvenManager.Instance.dragimage.sprite = image.sprite;
        UI_InvenManager.Instance.dragimage.transform.position = eventData.position;
        UI_InvenManager.Instance.dragimage.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �̰� ������ �� Ȯ�� �� �ʿ䰡 ���� 
        UI_InvenManager.Instance.dragimage.transform.position = eventData.position;
    }
}
