using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class re_Item
{


    // �ٽ� �ϸ鼭 ����? �����ؾ��� ��ϵ� 

    // 1. for�� �Ƴ��� (return break Ȯ����)
    // 2. Destroy Ÿ�̹� ��� �ָ��ϴ� ���� 
    // 3. Update ����
    // 4. ��ư�̳� ��ȣ�ۿ��� �ڷ�ƾ? (�����̹����� ������)

    // Item >> �� ������ �׳� �켱 DataManager���� ������ �������� ID�� �������Բ� �ϴ°� ����Ʈ ���� 

    // �׷� �ʵ忡 �ִ� �������� ��� Ȯ�� �Ұǵ�? 
    // �ű� ���� Component�� ID�� �־��ִ°�? ��¼�� �ű⼭�� �� �׷��͵� ���µ� 


    // �׷� ���� ��ӹ��� �ʴ� Item�� �� �����صΰ� ������ �Ŵ����� ������ �ְ� 

    // ������ ��ü �����͸� �ְ� 
    // ������ ���̺� �����ϰ� �������ִ� ����? 
    // >> �̷��� �Ǹ� ���̺��� ���� ������Ʈ ����? 
    // �̶� ���̺� Ÿ�ֿ̹� �������� �ɶ�? ������ ������ ���̺� ���־��ְ� �̰� ���� �ϴ� ���? 

    // �׷� ������ >>  ID �� ��������Ʈ�� �޾Ƽ� ǥ���ұ�? ���̵� �־ �������̼��� �� �� �����ϱ� 
    // �׷� ������ ?
    // ���� ��� ��ü�� ������ ������ ������ �ٸ��� �ƿ� ����?
    // invenslot ondrag 
    // quick drag   enum �� quick �̸� ������� �ϳ� �� ���شٴ� ��������? 


    // slot list > �־��ٶ� Item id ���� �ְ� sprite �����ͼ� ������ 


    // �����ڿ��� �޾ƿ´ٰ� ? 

    // ���̺���ü�� spritename�� ���� 
    // ������ȭ �Ҷ� Sprite ��ü�� 





    // �Ծ����� �κ��� �ְ� 
    // 1���� ������ ���� ���� ������ ������ ���� ���� ���� 

    public int id;
    public string name;
    public eItemType eItemType;
    public int elementindex;
    public int combineindex;
    public string tableName;
    public bool isused;
    public int usecount;
    public bool isfix;
    public Sprite sprite;


    public re_Item(int id, string name, int eItemType, int elementindex, 
                    int combineindex, string tableName, bool isused, int usecount, bool isfix, string spritename)
    {
        this.id = id;
        this.name = name;
        this.eItemType = (eItemType)eItemType;
        this.elementindex = elementindex;
        this.combineindex = combineindex;
        this.tableName = tableName;
        this.isused = isused;
        this.usecount = usecount;
        this.isfix = isfix;
        this.sprite = Resources.Load<Sprite>($"UI/Item/{spritename}");
        Debug.Log(this.sprite);
    }



}
