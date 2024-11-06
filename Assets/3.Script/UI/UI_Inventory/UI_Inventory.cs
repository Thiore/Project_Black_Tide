using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryslots;
    [SerializeField] private GameObject itemInformation;

    private List<Item> items;

    // UI_Press >> �巡���ϴ°� �ص� 

    // 1. �̳� Ÿ�Կ� ���� ������ ���� �κ� ���� 
    // 2. ���� SaveManager�� ���� ������ ���� �غ� �ϱ� 
    // 3. �����Ե� ��� ��Ű�� 

    // 1. �ٸ� ������Ʈ Ŭ�� �� ȭ�鿡 ���� �ϱ� 
    // 2. ���� 
    // 3. 3D������ 

    private void Awake()
    {
        items = new List<Item>();
    }

    public void GetItemTouch(Item item)
    {
        for (int i = 0; i < inventoryslots.Length; i++)
        {
            if (!inventoryslots[i].TryGetComponent(out Item notusethis))
            {
                Debug.Log("�κ� " + inventoryslots[i].name);
                inventoryslots[i].SetActive(true);
                Item itemUI = inventoryslots[i].AddComponent<Item>();
                itemUI.PutInInvenItem(item);

                if(itemUI.TryGetComponent(out Image sprite))
                {
                    sprite.sprite = itemUI.sprite;
                }

                Destroy(item.gameObject);
            }
            break;
        }

        items.Add(item);
    }



    public void CurrentHaveItem()
    {
        foreach(Item item in items)
        {
            item.ID 
        }
    }

}
