using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BtnInventory : MonoBehaviour
{
    [SerializeField] private GameObject btn;
    [SerializeField] private GameObject inventory;
    [SerializeField] private Toggle quickSlot;
    [Header("3D UI")]
    [SerializeField] private Camera UICam_3D; // Ȯ�� �� �ܼ����� ĸó ���� Camera
    [SerializeField] private RenderTexture renderTexture; // Ȯ�� �� ������Ʈ �� Pin���� �̹����� ����ϱ� ���� Render Texture
    [SerializeField] private GameObject pinObj;
    [SerializeField] private GameObject pinExit;
    [SerializeField] private GameObject keyClue;
    
    private Animator quickSlotAnim;

    private Toggle pinToggle = null; // 3D UI���� Pin�� ���� ���

    //inspectorâ���� Ȯ���ϱ� ���� public��� �Ʒ� ���� ���� ���� private���� ����
    public bool isPinItem = false; // ������Ʈ�� �ܼ� ���������� �ƴ��� ����



    private void Start()
    {
        quickSlot.TryGetComponent(out quickSlotAnim);
        pinObj.TryGetComponent(out pinToggle);

        //�ӽ�
        isPinItem = true;
    }
    public void OpenInventory()
    {
        GameManager.Instance.isInput = true;
            btn.SetActive(false);        
        
        inventory.SetActive(true);
    }

    public void CloseInventory()
    {
        GameManager.Instance.isInput = false;
        btn.SetActive(true);
        
        inventory.SetActive(false);
    }

    public void QuickSlot()
    {
        if(quickSlot.isOn)
        {
            quickSlotAnim.SetTrigger("Close");
        }
        else
        {
            quickSlotAnim.SetTrigger("Open");
        }
    }

    public void OnScaleItem(/*�κ��丮�� �����ִ� ������ �־��ּ���*/)
    {
        //����� Scale��ư�� OnClick�� �־�����ϴ�.
        if (isPinItem/*�������� �ܼ��������̶�� true��ȯ�ǰ��ϰ� isPin�� �ӽ�*/)
        {
            isPinItem = true;
            pinObj.SetActive(true);
            if (pinToggle.isOn)
            {
                keyClue.SetActive(true);
            }
            else
            {
                keyClue.SetActive(false);
            }
        }
        else
        {
            isPinItem = false;
            pinToggle.isOn = false;
            pinObj.SetActive(false);
            keyClue.SetActive(false);
        }

        if (keyClue.activeSelf)
        {
            //�ܼ������� �������� �� �ٸ� �ܼ� ������Ʈ�� �Ѵ� ��츦 ���� ���
            PinCapture();
        }
    }

    public void PinCapture()
    {
        if(keyClue.activeSelf&&isPinItem)
        {
            pinObj.SetActive(false);
            pinExit.SetActive(false);
            UICam_3D.targetTexture = renderTexture; // 3D UIī�޶��� Output Texture �߰�
            UICam_3D.Render(); // OutputTexture�� UICam ��� ���
            UICam_3D.targetTexture = null; // OutputTexture ����
            pinObj.SetActive(true);
            pinExit.SetActive(true);
        }
        
    }
}
