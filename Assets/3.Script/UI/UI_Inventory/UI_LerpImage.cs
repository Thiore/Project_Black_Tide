using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LerpImage : MonoBehaviour
{
    [SerializeField]private RectTransform inventoryButton; // �κ��丮 ��ư�� RectTransform
    [SerializeField]private Canvas canvas;
    
    private Vector2 invenPos;

    private RectTransform lerpImage;

    public Coroutine lerp_co { get; private set; } = null;

    public void Start()
    {
        TryGetComponent(out lerpImage);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            inventoryButton.anchoredPosition,
            null,
            out invenPos);
    }

    //�������� ��ġ�ص忡�� �� �Լ� �ҷ��ֽø� �˴ϴ�!
    public void OnLerpItem(Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            position,
            null,
            out Vector2 localPoint);

        if(lerp_co == null)
        {
            lerp_co = StartCoroutine(LerpImage_co(localPoint));
        }
    }

    private IEnumerator LerpImage_co(Vector2 localPoint)
    {
        float lerpTime = 0f;
        while(lerpTime.Equals(1f))
        {
            lerpTime += Time.deltaTime;
            if(lerpTime>1f)
            {
                lerpTime = 1f;
            }
            lerpImage.anchoredPosition = Vector2.Lerp(localPoint, invenPos, lerpTime);
            if(!lerpImage.gameObject.activeSelf)
            {
                lerpImage.gameObject.SetActive(true);
            }
            yield return null;
        }
        //���⼭ �κ��� ���� ó���ϸ� �ɰͰ����ϴ�!
        lerpImage.gameObject.SetActive(false);
        lerp_co = null;
        yield break;

    }
}
