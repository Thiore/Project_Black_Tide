using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueTrigger : MonoBehaviour
{
    [SerializeField] private int clueIndex;
    

    private GameObject playInterface;
    private GameObject clue;
    private GameObject clueItem;
    private GameObject exit;
    private GameObject uiCamera;

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
            GetClue();
        }
    }

    //�ܼ� ������Ʈ ����� ��, 3D_UI Ȱ��ȭ
    private void GetClue()
    {
        //�⺻ UI ��Ȱ��ȭ
        playInterface.gameObject.SetActive(false);
        uiCamera.gameObject.SetActive(true);

        //3D_UI Ȱ��ȭ
        exit.gameObject.SetActive(true);
        clueItem.gameObject.SetActive(true);

    }

    //���丮 ������Ʈ => �ش� ��ġ���� �ݺ������� ��ġ�� �� �ְ� / ���� (x)
    public void ExitStoryObj()
    {
        //3D_UI ��Ȱ��ȭ
        exit.gameObject.SetActive(false);
        clueItem.gameObject.SetActive(false);

        //�⺻ UI Ȱ��ȭ
        playInterface.gameObject.SetActive(true);
        uiCamera.gameObject.SetActive(false);

    }

    //�ܼ� ������Ʈ => �κ��丮�� ���� ������ / ����(O)
    public void ExitClueObj()
    {
        //3D_UI ��Ȱ��ȭ
        exit.gameObject.SetActive(false);
        clueItem.gameObject.SetActive(false);

        //�⺻ UI Ȱ��ȭ
        playInterface.gameObject.SetActive(true);
        uiCamera.gameObject.SetActive(false);


    }

    private void FindObjectUI()
    {


        if (clue != null)
        {
            Transform clueItem_ = clue.transform.GetChild(clueIndex);
            clueItem = clueItem_.gameObject;

            Transform exit_ = clue.transform.GetChild(3);
            exit = exit_.gameObject;

            //�ӽ÷� 5�� °���� ã��, �׽�Ʈ ������ ī�޶� ������� �ø��� clueIndex + 1 ��
            Transform camera_ = clue.transform.GetChild(4);
            uiCamera = camera_.gameObject;
        }
    }

    private void OnEnable()
    {
        playInterface = GameObject.FindGameObjectWithTag("PlayInterface");
        clue = GameObject.FindGameObjectWithTag("Clue");
        FindObjectUI();
    }
}
