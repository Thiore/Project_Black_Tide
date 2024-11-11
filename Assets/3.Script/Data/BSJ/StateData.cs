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
        [HideInInspector]
        public float[] audioSound;
        [HideInInspector]
        public float[] playerPos;
        [HideInInspector]
        public float[] playerRot;
        public GameState(List<FloorState> floors, float[] audioSound, Vector3 playerPos, Quaternion playerRot)
        {
            this.floors = floors;
            this.audioSound = audioSound;
            this.playerPos = new float[3];
            this.playerPos[0] = playerPos.x;
            this.playerPos[1] = playerPos.y;
            this.playerPos[2] = playerPos.z;

            this.playerRot = new float[4];
            this.playerRot[0] = playerRot.x;
            this.playerRot[1] = playerRot.y;
            this.playerRot[2] = playerRot.z;
            this.playerRot[3] = playerRot.w;
        }
        public GameState(List<FloorState> floors, Vector3 playerPos, Quaternion playerRot)
        {
            this.floors = floors;

            this.playerPos = new float[3];
            this.playerPos[0] = playerPos.x;
            this.playerPos[1] = playerPos.y;
            this.playerPos[2] = playerPos.z;

            this.playerRot = new float[4];
            this.playerRot[0] = playerRot.x;
            this.playerRot[1] = playerRot.y;
            this.playerRot[2] = playerRot.z;
            this.playerRot[3] = playerRot.w;
        }
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
