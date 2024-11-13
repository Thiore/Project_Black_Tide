using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    [SerializeField] private Image dragImage;
    [SerializeField] private Image itemInformation;
    private Item copyItem;
    private Coroutine dragcoroutine;
    private float downTime;

    public void OnDrag(PointerEventData eventData)
    {
        dragImage.transform.position = eventData.position;
    }

    private IEnumerator HoldDragStart(PointerEventData eventData)
    {
        while (downTime < 2f)
        {
            downTime += Time.fixedDeltaTime;
            yield return null;
        }

        DragStart(eventData);
        downTime = 0f;
    }

    private void DragStart(PointerEventData eventData)
    {
        //�� ��ġ���� Ȱ��ȭ 
        dragImage.transform.position = eventData.position;
        if (TryGetComponent(out copyItem))
        {
            dragImage.sprite = copyItem.Sprite;
            dragImage.gameObject.SetActive(true);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("�����۽��� ����Ʈ �ٿ�");

        if (TryGetComponent(out copyItem))
        {
            dragImage.sprite = copyItem.Sprite;
            dragImage.transform.position = eventData.position;
            dragImage.gameObject.SetActive(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragImage.gameObject.SetActive(false);
        Debug.Log("�巡�׿���");
        Debug.Log(eventData.pointerEnter.name);

        if(eventData.pointerEnter.TryGetComponent(out UI_ItemInformation info))
        {
            info.Combine(eventData);
        }

        //�� ���� ó�� �ص� 


    }

}
