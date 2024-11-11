using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryslots;
    [SerializeField] private GameObject[] quickSlots;

    private List<Item> myitems;

    // UI_Press >> �巡���ϴ°� �ص� 

    // 1. �̳� Ÿ�Կ� ���� ������ ���� �κ� ���� 
    // 2. ���� SaveManager�� ���� ������ ���� �غ� �ϱ� 
    // 3. �����Ե� ��� ��Ű�� 

    // 1. �ٸ� ������Ʈ Ŭ�� �� ȭ�鿡 ���� �ϱ� 
    // 2. ���� 
    // 3. 3D������ 

    private void Awake()
    {
        myitems = new List<Item>();
    }

    public void GetItemTouch(Item item)
    {
        Debug.Log("����ġ�ߵ�");
        switch (item.Type)
        {
            case eItemType.quick:
                Debug.Log("���ߵ�");
                AddItemQuick(item);
                break;

            case eItemType.element:
                Debug.Log("�κ��ߵ�");
                AddItemInventory(item);
                break;

            case eItemType.trigger:
                Debug.Log("xmflrj�ߵ�");
                AddItemQuick(item);
                break;

            case eItemType.clue:
                Debug.Log("clue �ߵ�");
                AddItemInventory(item);
                break;

        }
        myitems.Add(item);
    }


    public void AddItemInventory(Item item)
    {
        for (int i = 0; i < inventoryslots.Length; i++)
        {
            if (!inventoryslots[i].transform.TryGetComponent(out Item notusethis))
            {
                Debug.Log("�κ� " + inventoryslots[i].name);
                inventoryslots[i].SetActive(true);
                Item itemUI = inventoryslots[i].AddComponent<Item>();
                itemUI.PutInInvenItem(item);

                if (inventoryslots[i].transform.GetChild(0).TryGetComponent(out Image sprite))
                {
                    sprite.sprite = itemUI.Sprite;
                    Debug.Log(sprite.sprite.name);
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
                Debug.Log("�� " + quickSlots[i].name);
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

    public void CurrentHaveItem()
    {
        foreach (Item item in myitems)
        {

        }
    }

}
