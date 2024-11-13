using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LerpImage : MonoBehaviour, ITouchable
{
    [SerializeField] public RectTransform inventoryButton; // �κ��丮 ��ư�� RectTransform
    private Vector2 startPosition; // ��� ��ġ
    private Vector2 endPosition;   // ���� ��ġ (�κ��丮 ��ư ��ġ)
    private float lerpTime = 0f;   // Lerp ���൵
    private bool isLerping = false;

    private RectTransform rectTransform; // �����̹��� RectTransform

    private void Awake()
    {
        endPosition = inventoryButton.anchoredPosition;
    }

    public void StartLerp(Vector3 objectWorldPosition)
    {
        // 3D ������Ʈ�� ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectWorldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform, screenPosition, null, out startPosition
        );

        rectTransform.anchoredPosition = startPosition;
        lerpTime = 0f;
        isLerping = true;
    }

    private void Update()
    {
        if (isLerping)
        {
            lerpTime += Time.deltaTime * 2f; // �ӵ� ���� ����
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, lerpTime);

            if (lerpTime >= 1f)
            {
                isLerping = false;
                lerpTime = 0f;
            }
        }
    }

    public void OnTouchStarted(Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    public void OnTouchHold(Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    public void OnTouchEnd(Vector2 position)
    {
        throw new System.NotImplementedException();
    }
}
