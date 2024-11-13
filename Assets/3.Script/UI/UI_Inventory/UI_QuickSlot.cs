using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_QuickSlot : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler, IPointerUpHandler
{
    [SerializeField] private Image dragImage;
    private Item copyItem;
    private Coroutine dragcoroutine;
    private float downTime;

    // ����ϸ� �������� 
    private void Awake()
    {

    }
    

    public void OnDrag(PointerEventData eventData)
    {
        dragImage.transform.position = eventData.position;
        Debug.Log("�� �� �巡��");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("�� ����Ʈ �ٿ�");

        if (TryGetComponent(out copyItem))
        {
            if (copyItem.ID.Equals(2))
            {
                Debug.Log("�Ǥ�,�� ������������");
            }
            else
            {
                dragImage.sprite = copyItem.Sprite;
                dragImage.transform.position = eventData.position;
                dragImage.gameObject.SetActive(true);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (copyItem.ID.Equals(2))
        {
            // �̰ɷ� ţ���� ������ Ű�� ���� 
        }
        else
        {

        }

        if (dragImage.gameObject.activeSelf)
        {
            dragImage.gameObject.SetActive(false);
        }
        Debug.Log("�� �巡�׿���");
        Debug.Log("����ٰ� ��ȣ�ۿ�");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (copyItem.ID.Equals(2))
        {
            // �̰ɷ� ţ���� ������ Ű�� ���� 
        }
     
    }
}
