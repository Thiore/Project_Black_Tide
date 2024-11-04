using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    // �ڽ��� �ε����� ������
    [SerializeField] private int storyIndex;

    //3D_UI Object
    [SerializeField] private GameObject clue;

    //3D_UI
    private GameObject ui_3D;

    private bool isStory = false; // ���丮 ���� ��ȣ�ۿ� ����

    //private DialogueManager dialogueManager;

    private ReadInputData input;

    private void Start()
    {
        //DialougeManager ã��
        //dialogueManager = FindObjectOfType<DialogueManager>();

        //3D_UI ã��
        ui_3D = GameObject.FindGameObjectWithTag("3D_UI");

        //ReadInputData ã��
        TryGetComponent(out input);
        if (DialogueManager.Instance == null)
        {
            Debug.Log("���� ���� �ʾƿ�");
        }
    }

    private void Update()
    {
        if (input.isTouch)
        {
            GetClue();
        }
    }


    //���� Text ������
    private void StoryStart()
    {
        isStory = true; //��ȣ�ۿ� ���� 

        //��� ���
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.SetDialogue("Table_StoryB1", storyIndex);
        }


    }

    //�ܼ� ������Ʈ ����� ��, 3D_UI Ȱ��ȭ
    private void GetClue()
    {
        ui_3D.gameObject.SetActive(true);
    }
}
