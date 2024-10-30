using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DraggableItemTest : MonoBehaviour
{
    private Image sourceImage;
    public GameObject combineImage;
    private Vector3 originalPosition;
    private PlayerInput playerInput;
    private bool isHolding = false;

    private void Awake()
    {
        sourceImage = GetComponent<Image>();
        playerInput = GetComponent<PlayerInput>();

        //Hold Action ����
        playerInput.actions["Hold"].started += OnHoldStarted;
        playerInput.actions["Hold"].canceled += OnHoldCanceled;
    }

    private void OnHoldStarted(InputAction.CallbackContext context)
    {
        StartCoroutine(LongPress_co());
    }

    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
        combineImage.SetActive(false);
    }

    private IEnumerator LongPress_co()
    {
        yield return new WaitForSeconds(1f);
        isHolding = true;

        //CombineImage Ȱ��ȭ �� SourceImage����
        if (combineImage != null)
        {
            combineImage.SetActive(true);
            Image combineImageComponet = combineImage.GetComponent<Image>();
            combineImageComponet.sprite = sourceImage.sprite; //��ġ�� UI�� Sprite�� ����

            Color color = combineImageComponet.color;
            color.a = 0.7f; // �ణ �����ϰ�
            combineImageComponet.color = color;

            combineImage.transform.position = originalPosition; //��ġ ��ġ�� �̵�
        }

        while (isHolding)
        {
            combineImage.transform.position = Mouse.current.position.ReadValue();
            yield return null;
        }
    }

    private void CheckForDrop(PointerEventData eventData)
    {
        //Raycast�� UIüũ
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
                Image targetImage = result.gameObject.GetComponent<Image>();
                if (targetImage != null)
                {
                    //Sprite �̸����� Ȯ��
                    if (targetImage.sprite.name == "2")
                    {
                        sourceImage.sprite = Resources.Load<Sprite>("3");
                        targetImage.sprite = null;
                        targetImage.gameObject.SetActive(false);
                    }
                }
            }
        }

    }

    
    


}
