using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private int storyIndex; // �ڽ��� �ε����� ������
    private bool isStory = false; // ���丮 ���� ��ȣ�ۿ� ����
    private DialogueManager dialogueManager;
    private ReadInputData input;
    private void Start()
    {
        //DialougeManager ã��
        dialogueManager = FindObjectOfType<DialogueManager>();

        //ReadInputData ã��
        TryGetComponent(out input);
        if (dialogueManager == null)
        {
            Debug.Log("���� ���� �ʾƿ�");
        }
    }

    private void Update()
    {
        if (input.isTouch)
        {
            StoryStart();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isStory)
        {
            StoryStart();
        }
    }

    //���� Text ������
    private void StoryStart()
    {
        isStory = true; //��ȣ�ۿ� ���� 

        //��� ���
        if (dialogueManager != null)
        {
            dialogueManager.SetDialogue("Table_StoryB1", storyIndex);
        }
    }
}
