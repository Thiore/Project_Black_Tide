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

    public void Language(string sentence)
    {
        switch (sentence)
        {
            case "Korea":

                KoreaLanguageText.color = LanguageChoiceColor;
                EnglishLanguageText.color = Color.white;

                break;

            case "English":

                KoreaLanguageText.color = Color.white;
                EnglishLanguageText.color = LanguageChoiceColor;

                break;

            default:

                Debug.Log("���� ��� ���� �ε��� ����");

                break;
        }
    }
}