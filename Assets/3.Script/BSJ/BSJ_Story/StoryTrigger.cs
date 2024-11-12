using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour, ITouchable
{
    // �ڽ��� �ε����� ������
    [SerializeField] private int storyIndex;



    //���� Text ������
    private void StoryStart()
    {
        //isStory = true; //��ȣ�ۿ� ���� 

        //��� ���
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.SetDialogue("Table_StoryB1", storyIndex);
        }


    }

    public void OnTouchStarted(Vector2 position)
    {
        StoryStart();
    }

    public void OnTouchHold(Vector2 position)
    {
        
    }

    public void OnTouchEnd(Vector2 position)
    {
        
    }
}
