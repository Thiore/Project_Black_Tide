using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI KoreaLanguageText = null;
    [SerializeField] private TextMeshProUGUI EnglishLanguageText = null;

    [Header("��� ���� �� ������Ƽ")]
    [SerializeField] private Color LanguageChoiceColor;
    private void Start()
    {
        Language(0);
    }

    public void Language(int languageIndex)
    {
        switch (languageIndex)
        {
            case 1:

                KoreaLanguageText.color = LanguageChoiceColor;
                EnglishLanguageText.color = Color.white;
                DialogueManager.Instance.ChangeLocale(languageIndex);

                break;

            case 0:

                KoreaLanguageText.color = Color.white;
                EnglishLanguageText.color = LanguageChoiceColor;
                DialogueManager.Instance.ChangeLocale(languageIndex);
                break;

            default:

                Debug.Log("���� ��� ���� �ε��� ����");

                break;
        }
    }
}