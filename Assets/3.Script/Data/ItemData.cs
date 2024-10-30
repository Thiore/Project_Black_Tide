using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType
{
    element = 0,
    clue,
    trigger,
    quick
}

public class ItemData
{
    public int id;
    public int nametableid; 
    public eItemType type;
    public int elementindex;
    public int combineindex;
    public bool isfix;
    public string imgname;
}


// �������̼� ��ȯ�� �����ϳ� 
// ��������Ʈ�� �����ص� string �����ϳ�

public class Item : MonoBehaviour
{
    public int id;
    public int nametableid;
    public eItemType type;
    public int elementindex;
    public int combineindex;
    public bool isfix;
    public Sprite imgname;
}
