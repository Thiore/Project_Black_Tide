using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateData : MonoBehaviour
{
    //��ü
    [System.Serializable]
    public class GameState
    {
        public List<FloorState> floors; //��ü ���� ���¸� ��� ����Ʈ
    }

    //����
    [System.Serializable]
    public class FloorState
    {
        public int[] foorIndex; // �� ������ Index
        public List<InteractableObjectState> interactableObjects; //�ش� ���� ������Ʈ ���� ����Ʈ
    }

    //���� ��ȣ�ۿ� ������Ʈ
    [System.Serializable]
    public class InteractableObjectState
    {
        public int[] objectIndex; //�� ������Ʈ�� Index
        public bool isInteracted; // ��ȣ�ۿ� ����
    }
}
