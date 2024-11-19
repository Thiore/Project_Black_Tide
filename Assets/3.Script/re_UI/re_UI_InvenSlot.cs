using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class re_UI_InvenSlot : MonoBehaviour
{
    [SerializeField] private int id = -1;
    public int SlotID { get => id; }
    [SerializeField] private re_Item item;
    [SerializeField] private Image image;


    public void SetinvenByID(re_Item item)
    {
        id = item.id;
        this.item = item;
        image.sprite = item.sprite;
    }


    private void OnEnable()
    {
        if(id >= 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // 10�� Ǯ��
    // �ٵ� �ù� ���丮�� ��� �����Ұǵ� ������ 10������ ������ �վ�ߵ� ? 

}
