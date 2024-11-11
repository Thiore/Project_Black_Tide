using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryslots;
    [SerializeField] private GameObject[] quickSlots;

    [SerializeField] private GameObject invenBtnPos;
    [SerializeField] private GameObject quickBtnPos;
    [SerializeField] private Image lerpImage;

    private List<Item> myitems;

    // UI_Press >> �巡���ϴ°� �ص� 

    // 1. �̳� Ÿ�Կ� ���� ������ ���� �κ� ���� 
    // 2. ���� SaveManager�� ���� ������ ���� �غ� �ϱ� 
    // 3. �����Ե� ��� ��Ű�� 

    // 1. �ٸ� ������Ʈ Ŭ�� �� ȭ�鿡 ���� �ϱ� 
    // 2. ���� 
    // 3. 3D������ 


    // ������ ���� 0~3 �ִµ� 2 ���� ��ĭ�� �������� 

    private void Awake()
    {
        myitems = new List<Item>();
    }


    //������ Type�� ���� 
    public void GetItemTouch(Item item)
    {
        lerpImage.sprite = item.Sprite;
        lerpImage.transform.position = item.transform.position;
        if (!lerpImage.gameObject.activeSelf)
        {
            lerpImage.gameObject.SetActive(true);
        }
        Vector3.Lerp(lerpImage.transform.position, invenBtnPos.transform.position, 10f);


        switch (item.Type)
        {
            case eItemType.Quick:
                AddItemQuick(item);
                break;

            case eItemType.Element:
                AddItemInventory(item);
                break;

            case eItemType.Trigger:
                AddItemQuick(item);
                break;

            case eItemType.Clue:
                AddItemInventory(item);
                break;

            default:
                Debug.Log("�ƹ��͵� �ƴѰ�?");
                break;

        }

        myitems.Add(item);
        lerpImage.gameObject.SetActive(false);
    }


    public void AddItemInventory(Item item)
    {

        for (int i = 0; i < inventoryslots.Length; i++)
        {
            if (!inventoryslots[i].transform.TryGetComponent(out Item notusethis))
            {
                Item itemUI = inventoryslots[i].AddComponent<Item>();
                itemUI.PutInInvenItem(item);

                if (inventoryslots[i].transform.GetChild(0).TryGetComponent(out Image sprite))
                {
                    sprite.sprite = itemUI.Sprite;
                }

                Destroy(item.gameObject);
                break;
            }
        }
    }

    public void AddItemQuick(Item item)
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (!quickSlots[i].TryGetComponent(out Item notusethis))
            {
                quickSlots[i].SetActive(true);
                quickSlots[i].transform.GetChild(0).gameObject.SetActive(true);
                Item itemUI = quickSlots[i].AddComponent<Item>();
                itemUI.PutInInvenItem(item);

                if (itemUI.transform.GetChild(0).TryGetComponent(out Image sprite))
                {
                    sprite.sprite = itemUI.Sprite;
                }

                Destroy(item.gameObject);
                break;
            }
        }
    }

    public void GetCombineItem(Item item)
    {
        switch (item.Type)
        {
            case eItemType.Quick:
                AddItemQuick(item);
                break;

            case eItemType.Element:
                AddItemInventory(item);
                break;

            case eItemType.Trigger:
                AddItemQuick(item);
                break;

            case eItemType.Clue:
                AddItemInventory(item);
                break;

            default:
                Debug.Log("�ƹ��͵� �ƴѰ�?");
                break;

        }

        myitems.Add(item);
    }

    public void OpenInventory()
    {
        for(int i = 0; i < inventoryslots.Length; i++)
        {
            if (inventoryslots[i].TryGetComponent<Item>(out Item item))
            {
                inventoryslots[i].SetActive(true); 
            }
        }
    }

    //���̵� �˻��ؼ� �ִ��� ������ bool return 

}
