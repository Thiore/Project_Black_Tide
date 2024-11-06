using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad_Btn : MonoBehaviour
{
    [SerializeField] private InputField input;
    [SerializeField] private int answer;
    // ����1�� �� ������ ���� bool
    public bool isAnswer;

    //���� ���� ���� player camera�� ���ư��� ���� virtualCam
    [SerializeField] private GameObject virtualCam;

    public void BtnClick(Button clickedBtn)
    {
        string btnName = clickedBtn.name;
        Text btnText = clickedBtn.GetComponentInChildren<Text>();

        if(int.TryParse(btnText.text,out int result))
        {
            if (input.text.Length >= 3)
            {
                Debug.Log("Too many input");
                return;
            }
            input.text += btnText.text;
        }
        else
        {
            if (btnText.text.Equals("Enter"))
            {
                Debug.Log($"Answer is {input.text}");
                if (input.text.Equals(answer.ToString()))
                {
                    //���� ���� �ó׸ӽ� off
                    virtualCam.gameObject.SetActive(false);
                }
                input.text = "";
            }
            else
            {
                input.text=input.text.Substring(0, input.text.Length - 1);
            }
        }
    }
}
