using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Press : MonoBehaviour
{
    public InputActionAsset inputActionAsset;
    private InputAction press;
    private InputAction screenPos;
    private Vector3 curScreenPos; //���� Touch Position ����
    private Vector3 worldPos
    {
        get
        {
            //screen point�� world point�� ��ȯ
            //���� ȭ����ġ�� ���� ��ġ�� ��ȯ�ϰ� ����(������Ʈ���� ī�޶������ �Ÿ�)�� ����, 

            float z = UICam_Touch.WorldToScreenPoint(transform.position).z;
            return UICam_Touch.ScreenToWorldPoint(curScreenPos + new Vector3(0, 0, z));
        }
    }
    
    

    private Camera UICam_Touch;
    private bool isDragging; //  Drag���� ����
    private bool isTouchedOn //���� Touch ����
    {
        get
        {
            Ray ray = UICam_Touch.ScreenPointToRay(curScreenPos); //Camera���� Ray�� ��� ���� ȭ�� ��ġ �˱�
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // hit�� item�� �´��� Ȯ�� �� �´ٸ� true
            {
                return hit.transform == transform; 
            }
            return false; //�ƴ϶�� false ��ȯ
            
        }
    }

    private void Awake()
    {
        UICam_Touch = Camera.main;
        //Action Map ��������
        var uiActionMap = inputActionAsset.FindActionMap("UI");
        press = uiActionMap.FindAction("Press");
        screenPos = uiActionMap.FindAction("Screen Position");

        press.Enable();
        screenPos.Enable();

        //ȭ�� ��ġ ����� �� ���� �ݹ�(performed)context�� ���� ������ üũ
        //                                   ��> ���� ȭ�� ��ġ�� ��ġ�� ��ġ�� Vector2 ������ Update
        screenPos.performed += context_Screen => { curScreenPos = context_Screen.ReadValue<Vector2>(); };

        //Drag_co ����
        press.performed += _ => { if(isTouchedOn) StartCoroutine(Drag_co()); };
        press.canceled += _ => { isDragging = false; }; //Touch ���߸� false ��ȯ????
    }

    //Drag ����
    private IEnumerator Drag_co()
    {
        isDragging = true;
        Vector3 offset = transform.position - worldPos;
        
        //Grab
        while (isDragging)
        {
            //Dragging

            transform.position = worldPos + offset;
            yield return null;
        }
        //Drop
    }
}
