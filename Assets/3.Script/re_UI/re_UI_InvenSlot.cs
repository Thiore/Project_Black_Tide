using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class re_UI_InvenSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
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
    }

    public void SetInvenEmpty()
    {
        id = -1;
        item = null;
        image.sprite = null;
    }


    // ��, �ٿ� ���� �־�� ��� ��� ���� / eventsystem 50 ���� 
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("���Ӥ���");
        if (!isdragging)
        {
            UI_InvenManager.Instance.iteminfo.SetInfoByItem(item);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("�ٿ�");  
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
        // ���� ���δ� Infomation����, EndDrag �� PointerUp�̶� ���� �Ǽ� info �� OnDrop���� ó��
        UI_InvenManager.Instance.dragimage.transform.position = eventData.position;
    }

}
