 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

public class ToggleOBJ : InteractionOBJ, ITouchable
{
    [Header("SaveManager ����")]
    [SerializeField] protected int floorIndex;
    [SerializeField] protected int objectIndex;

    [Header("���� �� �ٸ�������Ʈ�� ��ȣ�ۿ��� �ʿ��ϸ� False")]
    [SerializeField] private bool isClear;

    protected override void Start()
    {
        base.Start();
        isTouching = false;
        TryGetComponent(out anim);
        if (!isClear)
        {
            isClear = SaveManager.Instance.PuzzleState(floorIndex, objectIndex);

        }
    }

    public void OnTouchStarted(Vector2 position)
    {
    }
    public void OnTouchHold(Vector2 position)
    {

    }
    public void OnTouchEnd(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, TouchManager.Instance.getTouchDistance, TouchManager.Instance.getTouchableLayer))
        {
            if (hit.collider.gameObject.Equals(gameObject))
            {
                if(!isClear)
                    isClear = SaveManager.Instance.PuzzleState(floorIndex, objectIndex);
               
                if (isClear)
                {
                    isTouching = !isTouching;
                    anim.SetBool(openAnim, isTouching);
                }
                else
                {
                    //"����־�"��� ���� ��� ���
                    DialogueManager.Instance.SetDialogue("Table_StoryB1", 1);
                    Debug.Log("���� �� ������?");
                }
                
            }
        }
    }
    public void ClearOpen()
    {
        isTouching = true;
       
    }
    
    
}
