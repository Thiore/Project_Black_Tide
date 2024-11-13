using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; } = null;

    private string savePath; //���� ���� ���
    public StateData.GameState gameState; //���� ���� ���� ��ü

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Json���� ���� ���
        savePath = Path.Combine(Application.persistentDataPath, "gameState.json");
        Debug.Log(savePath);

        //���� ���� �ʱ�ȭ (�� ����Ʈ �ʱ�ȭ)
        gameState = new StateData.GameState { floors = new List<StateData.FloorState>() };

    }
    //������ ��׶���� ���� ��, ����
    private void OnApplicationPause(bool pause)
    {
        //���ø����̼��� ��׶���� ���ų�, �ٽ� ���ƿ��� �� ȣ��
        if (pause)
        {
            SaveGameState();
        }
    }

    //���ø����̼��� ���� �Ǿ��� ��
    private void OnApplicationQuit()
    {
        SaveGameState();
    }

    //������
    public void NewGame()
    {
        //gameState �ʱ�ȭ (�� ����Ʈ)
        gameState = new StateData.GameState
        {
            floors = new List<StateData.FloorState>(),

            //�÷��̾� �ʱ�ȭ
            playerPositionX = 203.672f,
            playerPositionY = 1f,
            playerPositionZ = 2.91f,
            playerRotationX = 0,
            playerRotationY = 0,
            playerRotationZ = 0,
            playerRotationW = 1 // �⺻ ȸ�� ����

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
        //�÷��̾� ��ġ �� ȸ�� ����
        

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

    public void LoadPlayerPosition(Transform obj)
    {
        
        if (obj != null)
        {
            obj.transform.localPosition = new Vector3(
                gameState.playerPositionX,
                gameState.playerPositionY,
                gameState.playerPositionZ
                );
            obj.transform.localRotation = new Quaternion(
                gameState.playerRotationX,
                gameState.playerRotationY,
                gameState.playerRotationZ,
                gameState.playerRotationW
                );
        }

    }

    public void SavePlayerPosition(Transform obj)
    {
        
        if (obj != null)
        {
            gameState.playerPositionX = obj.transform.localPosition.x;
            gameState.playerPositionY = obj.transform.localPosition.y;
            gameState.playerPositionZ = obj.transform.localPosition.z;

            gameState.playerRotationX = obj.transform.localRotation.x;
            gameState.playerRotationY = obj.transform.localRotation.y;
            gameState.playerRotationZ = obj.transform.localRotation.z;
            gameState.playerRotationW = obj.transform.localRotation.w;
        }

    }

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    // ���� �÷��̰� ����Ǵ� B1F �������� LoadGameState ȣ��
    //    if (scene.name == "B1F 3") // B1F �� �̸��� ��Ȯ�ϰ� ���
    //    {
    //        LoadGameState();
    
    //    }
    //}

    //private IEnumerator test_co()
    //{
    //    GameObject player = GameObject.FindGameObjectWithTag("RealPlayer");
    //    if (player != null)
    //    {
    //        // ������ ��ġ�� �ݺ������� ����
    //        Vector3 targetPosition = new Vector3(
    //            gameState.playerPositionX,
    //            gameState.playerPositionY,
    //            gameState.playerPositionZ
    //        );

    //        Quaternion targetRotation = new Quaternion(
    //            gameState.playerRotationX,
    //            gameState.playerRotationY,
    //            gameState.playerRotationZ,
    //            gameState.playerRotationW
    //        );

    //        // ���� �ð� ���� �ݺ��Ͽ� ��ġ ����
    //        for (int i = 0; i < 5; i++) // 5ȸ �ݺ� ����
    //        {
    //            player.transform.localPosition = targetPosition;
    //            player.transform.localRotation = targetRotation;
    //            Debug.Log($"EnsurePlayerPosition - Reapply Position: {player.transform.position}");
    //            yield return new WaitForSeconds(0.1f); // ����
    //        }
    //    }

    //}
}
