using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueTrigger : MonoBehaviour, ITouchable
{
    [SerializeField] private int clueIndex;

    private GameObject mainPlayer;
    private GameObject playInterface;
    private GameObject clue;
    private GameObject clueItem;
    private GameObject exit;
    private GameObject uiCamera;
    private GameObject uiCanvas;


    //�ܼ� ������Ʈ ����� ��, 3D_UI Ȱ��ȭ
    private void GetClue()
    {
        Debug.Log("����");
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
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        if (mainPlayer != null)
        {
            //UI_Camera ã��
            Transform UI_Camera = mainPlayer.transform.GetChild(2);
            uiCamera = UI_Camera.gameObject;

            //Clue ã��
            Transform Clue_ = mainPlayer.transform.GetChild(3);
            clue = Clue_.gameObject;

            //UI_Canvas�� Exit ã��
            Transform UI_Canvas = mainPlayer.transform.GetChild(4);
            uiCanvas = UI_Canvas.gameObject;

            //Canvas�� PlayInterfaceã��
            Transform PlayInterface_ = mainPlayer.transform.GetChild(5);
            playInterface = PlayInterface_.gameObject;

            if (clue != null)
            {
                Transform clueItem_ = clue.transform.GetChild(clueIndex);
                clueItem = clueItem_.gameObject;

                Transform exit_ = uiCanvas.transform.GetChild(0);
                exit = exit_.gameObject;
            }
        }


    }

    private void OnEnable()
    {
        FindObjectUI();
    }

    public void OnTouchStarted(Vector2 position)
    {
        GetClue();
    }

    public void OnTouchHold(Vector2 position)
    {
    }

    public void OnTouchEnd(Vector2 position)
    {
        
    }
}
