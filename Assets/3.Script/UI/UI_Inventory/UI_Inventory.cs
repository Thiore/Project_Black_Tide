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
        switch (item.Type)
        {
            case eItemType.quick:
                AddItemQuick(item);
                break;

            case eItemType.element:
                AddItemInventory(item);
                break;

            case eItemType.trigger:
                AddItemQuick(item);
                break;

            case eItemType.clue:
                AddItemInventory(item);
                break;

        }
        myitems.Add(item);
    }


    public void AddItemInventory(Item item)
    {
        for (int i = 0; i < inventoryslots.Length; i++)
        {
            if (!inventoryslots[i].activeSelf)
            {
                Debug.Log("�κ� " + inventoryslots[i].name);
                inventoryslots[i].SetActive(true);
                Item itemUI = inventoryslots[i].AddComponent<Item>();
                itemUI.PutInInvenItem(item);

                if (itemUI.gameObject.transform.GetChild(0).TryGetComponent(out Image sprite))
                {
                    sprite.sprite = itemUI.Sprite;
                    Debug.Log(sprite.sprite.name);
                }

                Destroy(item.gameObject);
                break;
            }
            
            ////�� ������Ʈ ã�� 
            //if (!inventoryslots[i].TryGetComponent(out Item notusethis))
            //{
            //    Debug.Log("�κ� " + inventoryslots[i].name);
            //    inventoryslots[i].SetActive(true);
            //    Item itemUI = inventoryslots[i].AddComponent<Item>();
            //    itemUI.PutInInvenItem(item);

            //    if (itemUI.gameObject.transform.GetChild(0).TryGetComponent(out Image sprite))
            //    {
            //        sprite.sprite = itemUI.Sprite;
            //        Debug.Log(sprite.sprite.name);
            //    }

            //    Destroy(item.gameObject);
            //    break;
            //}
        }
    }

    public void AddItemQuick(Item item)
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (!quickSlots[i].activeSelf)
            {
                Debug.Log("�� " + quickSlots[i].name);
                quickSlots[i].SetActive(true);
                quickSlots[i].transform.GetChild(0).gameObject.SetActive(true);
                Item itemUI = quickSlots[i].AddComponent<Item>();
                itemUI.PutInInvenItem(item);

                if (itemUI.gameObject.transform.GetChild(0).TryGetComponent(out Image sprite))
                {
                    sprite.sprite = itemUI.Sprite;
                }

                Destroy(item.gameObject);
                break;
            }

            //�� ������Ʈ ã�� 
            //if (!quickSlots[i].TryGetComponent(out Item notusethis))
            //{
            //    Debug.Log("�� " + quickSlots[i].name);
            //    quickSlots[i].SetActive(true);
            //    quickSlots[i].transform.GetChild(0).gameObject.SetActive(true);
            //    Item itemUI = quickSlots[i].AddComponent<Item>();
            //    itemUI.PutInInvenItem(item);

            //    if (itemUI.gameObject.transform.GetChild(0).TryGetComponent(out Image sprite))
            //    {
            //        sprite.sprite = itemUI.Sprite;
            //    }

            //    Destroy(item.gameObject);
            //    break;
            //}
        }
    }

    public void CurrentHaveItem()
    {
        foreach (Item item in myitems)
        {

        }
    }

}
