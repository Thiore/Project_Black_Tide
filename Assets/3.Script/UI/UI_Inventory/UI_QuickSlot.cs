using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_QuickSlot : MonoBehaviour/*, IBeginDragHandler, IDragHandler*/
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

    private IEnumerator HoldDragStart(PointerEventData eventData)
    {
        while (downTime < 1.5f)
        {
            downTime += Time.fixedDeltaTime;
            yield return null;
        }

        DragStart(eventData);
        downTime = 0f;
        Debug.Log("�ڷ�ƾ�� ���� ��");
    }

    private void DragStart(PointerEventData eventData)
    {
        //�� ��ġ���� Ȱ��ȭ 
        dragImage.transform.position = transform.position;

        dragImage.gameObject.SetActive(true);
        dragImage.sprite = copyItem.Sprite;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       // dragcoroutine = StartCoroutine(HoldDragStart(eventData));
        Debug.Log("�� ����Ʈ �ٿ�");
    }
}
