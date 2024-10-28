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
            CheckForDrop(eventData);
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

    private void CheckForDrop(PointerEventData eventData)
    {
        // Raycast�� �浹�� UI üũ
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            //���̾� �˻�
            if (result.gameObject.layer == LayerMask.NameToLayer("QuickSlot"))
            {
                Image tartgetImage = result.gameObject.GetComponent<Image>();
                if (tartgetImage != null)
                {
                    //Sprite �̸����� Ȯ��
                    if (tartgetImage.sprite.name == "2")
                    {
                        sourceImage.sprite = Resources.Load<Sprite>("3");
                        
                        
                        tartgetImage.sprite = null;
                        tartgetImage.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
