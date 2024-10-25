using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("3D UI")]
    [SerializeField] private Camera UICam_3D; // Ȯ�� �� ������Ʈ ������ ���� Camera
    [SerializeField] private RenderTexture renderTexture; // Ȯ�� �� ������Ʈ �� Pin���� �̹����� ����ϱ� ���� Render Texture

    private Toggle pinToggle = null; // 3D UI���� Pin�� ���� ���

    //inspectorâ���� Ȯ���ϱ� ���� public��� �Ʒ� ���� ���� ���� private���� ����
    public bool isPinItem = false; // ������Ʈ�� �ܼ� ���������� �ƴ��� ����
    
    [SerializeField] private GameObject pinObj;

    private void Awake()
    {
        pinObj.TryGetComponent(out pinToggle);
        
        //�ӽ�
        isPinItem = true;
    }

    public void PinItem(/*�κ��丮�� �����ִ� ������ �־��ּ���*/)
    {
        //����� Scale��ư�� OnClick�� �־�����ϴ�.
        
         if(isPinItem/*�������� �ܼ��������̶�� true��ȯ�ǰ��ϰ� isPin�� �ӽ�*/)
         {
            isPinItem = true; 
            pinObj.SetActive(true);

         }
         else
         {
            if (pinToggle.isOn)
            { 
                //�ܼ����� ����
            }
            isPinItem = false;
            pinObj.SetActive(false);
         }
         
         if(isPinItem) 
        {
            //�ܼ������� �������� �� �ٸ� �ܼ� ������Ʈ�� �Ѵ� ��츦 ���� ���
            PinCapture();
        }

    }


    public void PinCapture()
    {
        UICam_3D.targetTexture = renderTexture; // 3D UIī�޶��� Output Texture �߰�
        UICam_3D.Render(); // OutputTexture�� UICam ��� ���
        UICam_3D.targetTexture = null; // OutputTexture ����
    }
}
