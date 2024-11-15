using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private UI_Inventory inventory;
    public UI_Inventory ui_inventory { get => inventory; }

    [SerializeField] private UI_ItemInformation itemInfo;
    public UI_ItemInformation ui_iteminfo {  get => itemInfo; }
    [SerializeField] private GameObject playerInterface;
    public GameObject getInterface { get => playerInterface; }

    [SerializeField] private UI_LerpImage lerpimage;
    public UI_LerpImage ui_lerpImage { get => lerpimage; }

    [SerializeField] private Transform mainPlayer;
    public Transform getMainPlayer { get => mainPlayer; }

    [SerializeField] private Transform playerCam;
    public Transform getPlayerCam { get => playerCam; }

    public Light flashLight;

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
