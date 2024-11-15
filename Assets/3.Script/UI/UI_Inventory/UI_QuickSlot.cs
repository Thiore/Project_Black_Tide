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
            PlayerManager.Instance.flashLight.enabled = !PlayerManager.Instance.flashLight.enabled;
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit, 4f, TouchManager.Instance.getTouchableLayer))
            {
                if (hit.collider.TryGetComponent(out TouchPuzzleCanvas toggle))
                {
                    for(int i = 0; i < toggle.getInteractionIndex.Length;i++)
                    {
                        if (copyItem.ID.Equals(toggle.getInteractionIndex[i]))
                        {
                            toggle.isInteracted = true;
                            SaveManager.Instance.UpdateObjectState(toggle.getFloorIndex, toggle.getInteractionIndex[i],true);
                            Debug.Log("����1?");
                            if (dragImage.gameObject.activeSelf)
                            {
                                dragImage.gameObject.SetActive(false);
                            }
                            CheckInteraction(toggle.getInteractionIndex[i]);
                        }
                    }
                }
                if(hit.collider.TryGetComponent(out PlayOBJ puzzle))
                {
                    for (int i = 0; i < puzzle.getObjectIndex.Length; i++)
                    {
                        if (copyItem.ID.Equals(puzzle.getObjectIndex[i]))
                        {
                            puzzle.InteractionCount();
                            SaveManager.Instance.UpdateObjectState(puzzle.getFloorIndex, puzzle.getObjectIndex[i], true);
                            Debug.Log("����2?");
                            if (dragImage.gameObject.activeSelf)
                            {
                                dragImage.gameObject.SetActive(false);
                            }
                            CheckInteraction(puzzle.getObjectIndex[i]);
                        }
                    }
                   
                }
            }
           
        }


        Debug.Log("�� �巡�׿���");
        Debug.Log("����ٰ� ��ȣ�ۿ�");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (TryGetComponent(out copyItem))
        {
            if (copyItem.ID.Equals(2))
            {
                PlayerManager.Instance.flashLight.enabled = !PlayerManager.Instance.flashLight.enabled;
            }
        }
    }

    //�̴ϰ��� / ��ȣ���� �ϱ����� ���̵� �˻��ؼ� �ִ��� ������ bool return 
    public void CheckInteraction(int id)
    {
        if (copyItem.ID.Equals(id))
        {
            if (copyItem.ID.Equals(id))
            {
                if(transform.GetChild(0).TryGetComponent(out Image sprite))
                {
                    sprite.sprite = null;
                    sprite.gameObject.SetActive(false);
                    Destroy(copyItem);
                    return;
                }
                

                
            }
        }
    }
}
