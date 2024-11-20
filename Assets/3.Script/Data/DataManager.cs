using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    private Dictionary<int, re_Item> dicItem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            InitDic();
            LoadAllData();
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += LoadSceanData; // 씬이 로드 될때마다 

    }


    private void InitDic()
    {
        dicItem = new Dictionary<int, re_Item>();
    }

    private void LoadAllData()
    {
        LoadItemData(); //원본 아이템 데이터 로드

    }

    // 데이터 역직렬화 후 바로 Item 타입으로 전환 
    private void LoadItemData()
    {
        string itemJson = Resources.Load<TextAsset>("Data/Json/Item_Data").text;
        dicItem = JsonConvert.DeserializeObject<re_Item[]>(itemJson).ToDictionary(x => x.id, x => x);
        Debug.Log(dicItem.Count);
    }


    private void LoadSceanData(Scene scene, LoadSceneMode mode)
    {

    }

    public re_Item GetItemInfoById(int id)
    {
        return dicItem[id];
    }

    public int GetItemElementIndex(int id)
    {
        return dicItem[id].elementindex;
    }

}
