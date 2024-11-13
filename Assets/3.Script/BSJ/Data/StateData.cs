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

        //�÷��̾� ��ġ
        public float playerPositionX;
        public float playerPositionY;
        public float playerPositionZ;

        //�÷��̾� ȸ��
        public float playerRotationX;
        public float playerRotationY;
        public float playerRotationZ;
        public float playerRotationW;
    }

    //����
    [System.Serializable]
    public class FloorState
    {
        public int floorIndex; // �� �� Index
        public List<InteractableObjectState> interactableObjects; //�ش� ���� ������Ʈ ���� ����Ʈ
    }

    //���� ��ȣ�ۿ� ������Ʈ
    [System.Serializable]
    public class InteractableObjectState
    {
        public int objectIndex; //�� ��ȣ�ۿ� ������Ʈ�� Index
        public bool isInteracted; // ��ȣ�ۿ� ����
    }
}
