using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class re_UI_QuickSlot : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private int id = -1;
    public int SlotID { get => id; }
    [SerializeField] private re_Item item;
    [SerializeField] private Image image;
    private bool isdragging = false;
    public void FragIsDrag()
    {
        isdragging = !isdragging;
    }

    public void SetinvenByID(re_Item item)
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
