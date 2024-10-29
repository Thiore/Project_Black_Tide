using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class LocalManager : MonoBehaviour
{
    private bool isChanging;
    public DialogueManager dia;
    public void ChangeLocale(int index)
    {
        if (isChanging)
            return;

        StartCoroutine(ChangeRoutine_co(index));
    }

    private IEnumerator ChangeRoutine_co(int index)
    {
        isChanging = true;

        yield return LocalizationSettings.InitializationOperation; //�ʱ�ȭ

        // ��� �ٲ��ֱ� SelectedLocale�� �ִ� ����
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        isChanging = false;

        dia.SetDialogue("B1", 2);
    }
}
