using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    private DialogueManager instance = null;
    public static DialogueManager Instance { get; private set; }

    //���丮 ���� UI Text
    public Button dialogueButton; //��� ��ư (��ġ ��, ������� �ϱ� ����)
    public TMP_Text dialogueText; //��� ǥ���� TextMeshPro
    private GameObject playInterface;

    //�κ��丮 ���� Text
    public TMP_Text itemName; //�κ��丮 ������ �̸� ��� TextMeshPro
    public TMP_Text explanation; //�κ��丮 ������ ���� ��� TextMeshPro

    //�ɼ� ���� Text
    public TMP_Text koreanButtonText; //�ѱ��� ��ư
    public TMP_Text englishButtonText; //���� ��ư
    private Color activeColor = Color.green; //Ȱ��ȭ ���� ��� �ؽ�Ʈ�� ����
    private Color inactiveColor = Color.white; //��Ȱ��ȭ ���� ��� �ؽ�Ʈ�� ����

    private LocalizedString localizedString = new LocalizedString();
    private LocalizedString itemNameLocalizedString = new LocalizedString();
    private LocalizedString itemExplanationLocalizedString = new LocalizedString();

    private bool isChanging; //��� ����
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Instance = instance;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
       
    }

    private void OnEnable()
    {
        // �� �ε� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
        if(PlayerManager.Instance != null)
            playInterface = PlayerManager.Instance.getInterface;
    }

    private void OnDisable()
    {
        // �� �ε� �̺�Ʈ ���
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //�� ���� �ε�� �� ȣ��
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateButtonColorByLocale();
    }

    //Story ���̺� �̸��� Ű���� ���� ��� ���
    public void SetDialogue(string tableName, int key)
    {
        localizedString.TableReference = tableName; //���̺� �̸� ��������
        localizedString.TableEntryReference = key.ToString();
        localizedString.StringChanged += UpdateDialogueText;
        localizedString.RefreshString(); //������ ���ڿ� ������Ʈ

        StartCoroutine(StoryBottonState_co());
    }
    private void UpdateDialogueText(string text)
    {
        dialogueText.text = text;

    }



    private IEnumerator StoryBottonState_co()
    {
        if (playInterface != null)
        {
            //2�� ���� ��ư ��Ȱ��ȭ
            dialogueButton.interactable = false;
            dialogueButton.gameObject.SetActive(true); //��ư Ȱ��ȭ
            //playInterface.gameObject.SetActive(false); //�������̽� ��Ȱ��ȭ
            yield return new WaitForSeconds(2f);

            //2�� �� ��ư Ȱ��ȭ �� ��ġ �̺�Ʈ
            dialogueButton.interactable = true;
            dialogueButton.onClick.AddListener(OnButtonClicked);

            //if (!dialogueButton.gameObject.activeSelf)
            //{
            //    playInterface.gameObject.SetActive(true); //�������̽� Ȱ��ȭ
            //}
        }


        //7�� �� �ڵ� ��Ȱ��ȭ(��ġ�� ��Ȱ��ȭ���� �ʾ��� ���)
        //yield return new WaitForSeconds(4f); //3�� ��� �� 4�� �� ���
        //dialogueButton.gameObject.SetActive(false);
    }

    private void OnButtonClicked()
    {
        //��ư ��ġ �� ��� ��Ȱ��ȭ
        dialogueButton.gameObject.SetActive(false);

        //��ġ �̺�Ʈ ����
        dialogueButton.onClick.RemoveListener(OnButtonClicked);
    }

    //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�

    //�κ��丮 Item Id���� Localization key �� ��ġ�ϰ� ����� ���
    public void SetItemNameText(string tableName, int key)
    {
        itemNameLocalizedString.TableReference = tableName; //���̺� �̸� ��������
        itemNameLocalizedString.TableEntryReference = key.ToString();
        itemNameLocalizedString.StringChanged += UpdateItemnameText;
        itemNameLocalizedString.RefreshString(); //������ ���ڿ� ������Ʈ
    }
    public void SetItemExplanationText(string tableName, int key)
    {
        itemExplanationLocalizedString.TableReference = tableName; //���̺� �̸� ��������
        itemExplanationLocalizedString.TableEntryReference = key.ToString();
        itemExplanationLocalizedString.StringChanged += UpdateItemExplanationText;
        itemExplanationLocalizedString.RefreshString(); //������ ���ڿ� ������Ʈ
    }
    private void UpdateItemnameText(string text)
    {
        itemName.text = text;
    }
    private void UpdateItemExplanationText(string text)
    {
        explanation.text = text;
    }

    //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
    private void OnDestroy()
    {
        localizedString.StringChanged -= UpdateDialogueText; //������ ����
    }


    
    //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�

    //��� ����
    public void ChangeLocale(int index)
    {
        if (isChanging)
            return;

        StartCoroutine(ChangeRoutine_co(index));

        
    }

    private IEnumerator ChangeRoutine_co(int index)
    {
        isChanging = true;

        yield return LocalizationSettings.InitializationOperation; //�ʱ�ȭ

        // ��� �ٲ��ֱ� SelectedLocale�� �ִ� ����
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        isChanging = false;

        //���� �� ���� ��ư ���� ������Ʈ
        UpdateButtonTextColor(index);

        
    }

    //���� ������Ʈ
    private void UpdateButtonTextColor(int index)
    {
        if (index == 1)
        {
            koreanButtonText.color = activeColor;
            englishButtonText.color = inactiveColor;
        }
        else if (index == 0)
        {
            koreanButtonText.color = inactiveColor;
            englishButtonText.color = activeColor;
        }
    }

    
   
    private void UpdateButtonColorByLocale()
    {
        // ���� Locale�� �ε����� ������ ��ư ���� ����
        int currentLocaleIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        UpdateButtonTextColor(currentLocaleIndex);
    }

}
