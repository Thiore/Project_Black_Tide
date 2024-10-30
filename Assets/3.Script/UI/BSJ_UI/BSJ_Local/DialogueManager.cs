using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText; //��� ǥ���� TextMeshPro
    private LocalizedString localizedString = new LocalizedString();

    // ���̺� �̸��� Ű���� ���� ��� ���
    public void SetDialogue(string tableName, int key)
    {
        localizedString.TableReference = tableName; //���̺� �̸� ��������
        localizedString.TableEntryReference = key.ToString();
        localizedString.StringChanged += UpdateDialogueText;
        localizedString.RefreshString(); //������ ���ڿ� ������Ʈ
    }

    private void UpdateDialogueText(string text)
    {
        dialogueText.text = text;
    }

    private void OnDestroy()
    {
        localizedString.StringChanged -= UpdateDialogueText; //������ ����
    }
}
