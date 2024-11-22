using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; } = null;

    [SerializeField] private AudioMixer audioMixer;
    [HideInInspector]
    public float master;
    [HideInInspector]
    public float BGM;
    [HideInInspector]
    public float SFX;
    [HideInInspector]
    public float CameraSpeed;

    [SerializeField] private GameObject settingPage;

    public TMP_Text koreanButtonText;//�ѱ��� ��ư

    public TMP_Text englishButtonText;//�ѱ��� ��ư

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //���־� ������Ź�����? + Settings SaveComplete�κп� �����ϴ°� �־��ֽø�˴ϴ�.
        //settingâ ���¹�ư�� �� �޼���� �߰��ص׾��~
        master = SaveManager.Instance.gameState.masterVolume;
        BGM = SaveManager.Instance.gameState.bgmVoluem;
        SFX = SaveManager.Instance.gameState.sfxVoluem;
        CameraSpeed = SaveManager.Instance.gameState.camSpeed;

        float masterValue = Mathf.Lerp(-60f, 0f, master);
        audioMixer.SetFloat("Master", masterValue);

        float BGMValue = Mathf.Lerp(-60f, 0f, BGM);
        audioMixer.SetFloat("BGM", BGMValue);
       
        float SFXValue = Mathf.Lerp(-60f, 0f, SFX);
        audioMixer.SetFloat("SFX", SFXValue);
    }

    public void OnSettingPage()
    {
        settingPage.SetActive(true);
    }
}
