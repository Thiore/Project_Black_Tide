using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, ITouchable, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    [SerializeField] private int id;
    [SerializeField] private eItemType type;
    [SerializeField] private int elementindex;
    [SerializeField] private int combineindex;
    [SerializeField] private string tableName;
    [SerializeField] private bool isfix;
    [SerializeField] private string spritename;

    public int ID { get => id; }
    public eItemType Type { get => type; }
    public int Elementindex { get => elementindex; }
    public int Combineindex { get => combineindex; }


    [SerializeField] private UI_Inventory inven;
    [SerializeField] private UI_ItemInformation iteminfo;

    private bool isUI;
    private bool isUsed;
    private bool isDrag;
    public void SetboolDrag()
    {
        isDrag = !isDrag;
    }

    private Sprite sprite;
    public Sprite Sprite { get => sprite; private set => sprite = Sprite; }



    private Vector2 firstPos;

    private void Awake()
    {
        isUI = true;


    }
    private void Start()
    {
        inven = PlayerManager.Instance.ui_inventory;
        iteminfo = PlayerManager.Instance.ui_iteminfo;
        // �̰� Ȱ��ȭ�� ã���� ���� ����ߵ� 
    }


    //private void Update()
    //{
    //if (isUI)
    //{
    //    inven.GetItemTouch(this);

    //    Debug.Log("������ update");
    //}
    //}

    public void InputItemInfomationByID(int id, ItemData data)
    {
        this.id = id;
        type = (eItemType)data.eItemType;
        elementindex = data.elementindex;
        combineindex = data.combineindex;
        tableName = data.tableName;
        isfix = data.isfix;
        spritename = data.spritename;
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public void PutInInvenItem(Item item)
    {
        this.id = item.id;
        type = item.type;
        elementindex = item.elementindex;
        combineindex = item.combineindex;
        tableName = item.tableName;
        isfix = item.isfix;
        spritename = item.spritename;
        sprite = item.sprite;
    }

    public void SetInventoryInfomation()
    {
        iteminfo.SetInfoByID(this);
    }


    public void OnTouchStarted(Vector2 position)
    {
        firstPos = position;//��ġ ������ġ ����
       
    }

    public void OnTouchHold(Vector2 position)
    {
        
    }

    public void OnTouchEnd(Vector2 position)
    {
        Debug.Log("��ġ����");
        //��ġ ������ ��ġ�� ������ġ���� �����Ÿ� ������������ �ƹ��ϵ� ���Ͼ���� �ϱ����� ���
        float touchUpDelta = Vector2.Distance(firstPos, position);
        //�Ÿ��� ������ �������� 50���� �ӽ÷� �����߽��ϴ� ���� ������ �ʿ��մϴ�.
        if (touchUpDelta<50f&&gameObject.CompareTag("Item3D"))
        {
            inven.GetItemTouch(this);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        firstPos = eventData.position;
        Debug.Log("�����ʹٿ�");
        if (gameObject.CompareTag("Item2D"))
        {
            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //��ġ ������ ��ġ�� ������ġ���� �����Ÿ� ������������ �ƹ��ϵ� ���Ͼ���� �ϱ����� ���
        float touchUpDelta = Vector2.Distance(firstPos, eventData.position);
        //�Ÿ��� ������ �������� 50���� �ӽ÷� �����߽��ϴ� ���� ������ �ʿ��մϴ�.
        if (touchUpDelta < 50f && gameObject.CompareTag("Item2D") && !isDrag)
        {
            SetInventoryInfomation();
            if (!iteminfo.gameObject.activeSelf)
            {
                iteminfo.gameObject.SetActive(true);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }


}
