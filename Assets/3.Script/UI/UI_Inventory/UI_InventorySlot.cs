using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image dragImage;
    [SerializeField] private Image itemInformation;
    private Item copyItem;
    private Coroutine dragcoroutine;
    private float downTime;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(TryGetComponent(out copyItem))
        {
            itemInformation.sprite = copyItem.Sprite;           
        }

        dragcoroutine = StartCoroutine(HoldDragStart(eventData));

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragImage.transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragImage.gameObject.SetActive(false);
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
}
