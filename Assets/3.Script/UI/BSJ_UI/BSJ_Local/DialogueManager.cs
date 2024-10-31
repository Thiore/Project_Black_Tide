using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Button dialogueButton; //��� ��ư (��ġ ��, ������� �ϱ� ����)
    public TMP_Text dialogueText; //��� ǥ���� TextMeshPro
    private LocalizedString localizedString = new LocalizedString();

    // ���̺� �̸��� Ű���� ���� ��� ���
    public void SetDialogue(string tableName, int key)
    {
        localizedString.TableReference = tableName; //���̺� �̸� ��������
        localizedString.TableEntryReference = key.ToString();
        localizedString.StringChanged += UpdateDialogueText;
        localizedString.RefreshString(); //������ ���ڿ� ������Ʈ

        StartCoroutine(StoryBottonState_co());
    }

    private void UpdateDialogueText(string text)
    {
        dialogueText.text = text;

        //�ؽ�Ʈ ���뿡 ���� ũ�� ����
        //StartCoroutine(DialogueSize_co());
    }

    private IEnumerator StoryBottonState_co()
    {
        //3�� ���� ��ư ��Ȱ��ȭ
        dialogueButton.interactable = false;
        dialogueButton.gameObject.SetActive(true); //��ư Ȱ��ȭ
        yield return new WaitForSeconds(3f);

        //3�� �� ��ư Ȱ��ȭ �� ��ġ �̺�Ʈ
        dialogueButton.interactable = true;
        dialogueButton.onClick.AddListener(OnButtonClicked);

        //7�� �� �ڵ� ��Ȱ��ȭ(��ġ�� ��Ȱ��ȭ���� �ʾ��� ���)
        yield return new WaitForSeconds(4f); //3�� ��� �� 4�� �� ���
        dialogueButton.gameObject.SetActive(false);
    }

    private void OnButtonClicked()
    {
        //��ư ��ġ �� ��� ��Ȱ��ȭ
        dialogueButton.gameObject.SetActive(false);

        //��ġ �̺�Ʈ ����
        dialogueButton.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        localizedString.StringChanged -= UpdateDialogueText; //������ ����
    }


    //�ӽ÷� ����� �׽��ϴ�, ���߿� �׳� ũ�� ���� ��Ű�� ����??�̸� ����
    private IEnumerator DialogueSize_co()
    {
        yield return null; //������ ���

        RectTransform rectTransform = dialogueText.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(dialogueText.preferredWidth, dialogueText.preferredHeight);
    }

    //private void TextFinish()
    //{
    //    dialogueText.gameObject.SetActive(false);
    //}
}
