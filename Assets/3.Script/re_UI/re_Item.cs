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

    public int id;
    public string name;
    public int eItemType;
    public int elementindex;
    public int combineindex;
    public string tableName;
    public bool isused;
    public int usecount;
    public bool isfix;
    public string spritename;



}
