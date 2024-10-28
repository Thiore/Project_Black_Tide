using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image sourceImage;
    public GameObject combineImage;
    private Vector3 originalPosition;

    private void Awake()
    {
        sourceImage = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = eventData.position; //�ʱ� ��ġ ��ġ ����
        StartCoroutine(LongPress_Co(eventData));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(LongPress_Co(eventData));

        if (combineImage != null)
        {
            combineImage.SetActive(false);
        }
    }

    private IEnumerator LongPress_Co(PointerEventData eventData)
    {
        yield return new WaitForSeconds(1f);

        //CombineImage Ȱ��ȭ �� SourceImage ����
        if (combineImage != null)
        {
            combineImage.SetActive(true);
            Image combineImageComponet = combineImage.GetComponent<Image>();
            combineImageComponet.sprite = sourceImage.sprite; //��ġ�� UI�� Sprite�� ����

            Color color = combineImageComponet.color;
            color.a = 0.7f;
            combineImageComponet.color = color;

            combineImage.transform.position = originalPosition; //��ġ ��ġ�� �̵�
        }

        while (true)
        {
            combineImage.transform.position = eventData.position;
            yield return null;
        }
    }
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    //originalPosition = rectTransform.anchoredPosition;
    //    quickSlot.alpha = 0.6f; //�巡�� �� ������
    //    quickSlot.blocksRaycasts = false; //�ٸ� UI Ŭ�� ����
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    rectTransform.anchoredPosition += eventData.delta; //�巡�� ��ġ ������Ʈ
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    quickSlot.alpha = 1f; //�巡�� ���� �� ������
    //    quickSlot.blocksRaycasts = true; //�ٽ� Ŭ�� ����
    //}
}
