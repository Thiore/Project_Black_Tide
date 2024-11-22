 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using TMPro;

public class StoryBox_Loading : MonoBehaviour
{
    public TMP_Text loadingText; // "..." �ݺ��� Text
    private Coroutine loading_Co;

    private void OnEnable()
    {
        if (loading_Co == null)
        {
            loading_Co = StartCoroutine(UpdateLoadingText_co());
        }
    }

    private void OnDisable()
    {
        if (loading_Co != null)
        {
            StopCoroutine(loading_Co);
            loading_Co = null;
        }

        if (loadingText != null)
        {
            loadingText.text = ""; //�ؽ�Ʈ �ʱ�ȭ
        }
    }

    private IEnumerator UpdateLoadingText_co()
    {
        string[] dots = { ".", "..", "..." };
        int index = 0;
        float addTime = 0f; //�ð� �߰�
        float duration = 2f; //2�� ���� �ݺ�

        while (addTime < duration)
        {
            loadingText.text = dots[index]; //Text ������Ʈ
            index = (index + 1) % dots.Length; //�ε��� ��ȯ
            addTime += 0.5f;
            yield return new WaitForSeconds(0.5f); //2�� �ֱ⸦ ���� 0.5�� ���
        }

        //3�� �� "..." ���·� ����
        loadingText.text = "...";
        loading_Co = null;
    }
}
