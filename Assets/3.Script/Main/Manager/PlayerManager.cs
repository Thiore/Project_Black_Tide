using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    //�κ��丮
    [SerializeField] private UI_Inventory inventory;
    public UI_Inventory ui_inventory { get => inventory; }

    //������ Information
    [SerializeField] private UI_ItemInformation itemInfo;
    public UI_ItemInformation ui_iteminfo {  get => itemInfo; }

    //������ �� 
    [SerializeField] private UseButton quickSlot;
    public UseButton getQuickSlot { get => quickSlot; }
    public Transform playerInterface { get => quickSlot.transform.parent; }


    [SerializeField] private UI_LerpImage lerpimage;
    public UI_LerpImage ui_lerpImage { get => lerpimage; }


    [SerializeField] private GameObject btnList;
    public GameObject getBtnList { get => btnList; }

    [SerializeField] private TMP_Text itemName;//�κ��丮 ������ �̸� ��� TextMeshPro
    public TMP_Text getItemName { get => itemName; }

    [SerializeField] private TMP_Text explanation;//�κ��丮 ������ ���� ��� TextMeshPro
    public TMP_Text getExplanation { get => explanation; }



    #region addTeo
    [SerializeField] private TeoLerp lerpImage;
    public TeoLerp getLerpImage { get => lerpImage; }

    public Transform mainPlayer;
    public Transform playerCam;

    public Light flashLight;
    [SerializeField] private Inventory inven;
    public Inventory getInven { get => inven; }
    #endregion


    public Button optionBtn;

    private void Awake()
    {
        Instance = this;

        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
}
