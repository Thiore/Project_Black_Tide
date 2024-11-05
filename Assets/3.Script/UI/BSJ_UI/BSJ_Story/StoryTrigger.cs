using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    // �ڽ��� �ε����� ������
    [SerializeField] private int storyIndex;

    private ReadInputData input;

    private void Start()
    {
        //ReadInputData ��������
        TryGetComponent(out input);   
    }

    private void Update()
    {
        if (input.isTouch)
        {
            StoryStart();
        }
    }


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

    

   

   
}
