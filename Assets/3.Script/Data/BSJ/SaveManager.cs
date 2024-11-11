using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    private SaveManager instance = null;
    public SaveManager Instance { get; private set; }

    private string savePath; //���� ���� ���
    public StateData.GameState gameState; //���� ���� ���� ��ü

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

        // Json���� ���� ���
        savePath = Path.Combine(Application.persistentDataPath, "gameState.json");

        //���� ���� �ʱ�ȭ (�� ����Ʈ �ʱ�ȭ)
        gameState = new StateData.GameState { floors = new List<StateData.FloorState>() };
    }

    //������
    public void NewGame()
    {
        //gameState �ʱ�ȭ (�� ����Ʈ)
        gameState = new StateData.GameState
        {
            floors = new List<StateData.FloorState>()
        };

        // �� ���� ��ȣ�ۿ� ������Ʈ �ʱ�ȭ �� �⺻ ���� ���� (�ӽ÷� 4 �س���)
        for (int floorIndex = 0; floorIndex < 4; floorIndex++)
        {
            // �� ������ �ʱ�ȭ �� �� �ε��� �� ������Ʈ ���� ����Ʈ ����
            StateData.FloorState floor = new StateData.FloorState
            {
                //���� �� �ε��� ����
                floorIndex = floorIndex,
                //�� �� ������Ʈ ����Ʈ �ʱ�ȭ
                interactableObjects = new List<StateData.InteractableObjectState>()
            };

            // �� �� ��ȣ�ۿ� ������Ʈ �ʱ�ȭ (�ӽ÷� 5 �س���)
            for (int objectIndex = 0; objectIndex < 5; objectIndex++)
            {
                //������Ʈ ���� �ʱ�ȭ(��ȣ�ۿ���� ���� ���·�)
                StateData.InteractableObjectState objState = new StateData.InteractableObjectState
                {
                    //������Ʈ �ε��� ����
                    objectIndex = objectIndex,
                    //��ȣ�ۿ���� ���� ����
                    isInteracted = false
                };
                //�ʱ�ȭ�� ������Ʈ ���¸� ���� �߰�
                floor.interactableObjects.Add(objState);
            }
            //�ʱ�ȭ �� �� ���¸� ���� ���¿� �߰�
            gameState.floors.Add(floor);
        }

    }

    // ���� �ε�
    public void LoadGameState()
    {
        //����� Json������ ���� �ϴ� ��� �ε�
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            //Json ���ڿ��� GameState ��ü�� �Ҵ�
            gameState = JsonConvert.DeserializeObject<StateData.GameState>(json);
        }
    }

    // ���� ���� ����
    public void SaveGameState()
    {
        string json = JsonConvert.SerializeObject(gameState, Formatting.Indented);
        File.WriteAllText(savePath, json);
    }

    // ���� ������Ʈ (�� �� ������Ʈ ���� ������Ʈ)
    public void UpdateObjectState(int floorIndex, int objectIndex, bool isInteracted)
    {
        //�ش� ���� ã�ų� ���� ����(������)
        StateData.FloorState floor = gameState.floors.Find(f => f.floorIndex == floorIndex);

        //�ش� ���� ���� ��� ���ο� �� �߰�
        if (floor == null)
        {
            floor = new StateData.FloorState
            {
                //�� �ε��� ����
                floorIndex = floorIndex,
                //������Ʈ ����Ʈ �ʱ�ȭ
                interactableObjects = new List<StateData.InteractableObjectState>()
            };
            //������ ���� floors ����Ʈ�� �߰�
            gameState.floors.Add(floor);
        }

        //�ش� ������Ʈ�� ã�ų� ���� �����Ͽ� ���� ������Ʈ
        StateData.InteractableObjectState objState = floor.interactableObjects.Find(obj => obj.objectIndex == objectIndex);

        //������Ʈ�� �������� ���� ��� ���ο� �κ���Ʈ �߰�
        if (objState == null)
        {
            objState = new StateData.InteractableObjectState
            {
                //������Ʈ �ε��� ����
                objectIndex = objectIndex,
                //���޵� ��ȣ�ۿ� ���� ����
                isInteracted = isInteracted
            };
            //������ ������ƮinteractableObjects ����Ʈ�� �߰�
            floor.interactableObjects.Add(objState);
        }
        else
        {
            //������Ʈ�� �̹� �����ϴ� ��� -> ��ȣ�ۿ� ���¸� ������Ʈ
            objState.isInteracted = isInteracted;
        }

        //����� ���� Json ���Ͽ� ����
        SaveGameState();

    }

    //puzzle�� ��ȣ�ۿ��ϴ� door�� ���� �˸���
    public bool PuzzleState(int floorIndex, int objectIndex)
    {
        StateData.FloorState floor = gameState.floors.Find(f => f.floorIndex == floorIndex);

        if (floor != null)
        {
            StateData.InteractableObjectState objState = floor.interactableObjects.Find(obj => obj.objectIndex == objectIndex);
            if (objState != null)
            {
                return objState.isInteracted;
            }
        }

        return false;
    }
}
