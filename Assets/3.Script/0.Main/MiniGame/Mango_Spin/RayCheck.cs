using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCheck : MonoBehaviour
{
    public TileRay startingTileRay; // ���� ������Ʈ�� TileRay ������Ʈ
    public List<GameObject> targetObjects; // ��� Ÿ�� ������Ʈ ����Ʈ
    public bool isComplete;
    public List<GameObject> connectedObjects;

    // ���� ���¸� Ȯ���ϴ� �޼���
    public bool CheckConnections()
    {
        if (startingTileRay == null)
        {
            Debug.LogError("Starting TileRay is not assigned!");
            return false;
        }

        connectedObjects.Clear(); // Ŭ���� ���� ���� ����� �ʱ�ȭ
        RecursiveConnectionCheck(startingTileRay, null, connectedObjects);

        // ���� Ÿ���� ������ Ȯ��
        isComplete = AreAllTargetsConnected(connectedObjects);
        return isComplete;
    }

    // ��������� ���� ���� Ȯ�� (����� ���� �ʼ�)
    private void RecursiveConnectionCheck(TileRay tileRay, GameObject previousObject, List<GameObject> connectedObjects)
    {
        connectedObjects.Add(tileRay.gameObject); // ���� ������Ʈ�� ���� ��Ͽ� �߰�

        List<GameObject> hitObjects = tileRay.GetHitObject(); // ���� ������Ʈ�� ������ ����� ������Ʈ ���

        foreach (var hitObject in hitObjects)
        {
            Debug.Log($"hit object : {hitObject.name}");
            // ���� hitObject�� ���� ������Ʈ���� ����� ������ Ȯ��
            TileRay hitTileRay = hitObject.GetComponent<TileRay>();

            if (hitTileRay != null && !connectedObjects.Contains(hitObject))
            {
                // hitObject�� ���� ������Ʈ�� ������ �Ǿ� �ִ��� Ȯ��
                if (previousObject == null || IsMutuallyConnected(hitTileRay, tileRay.gameObject))
                {
                    RecursiveConnectionCheck(hitTileRay, tileRay.gameObject, connectedObjects);
                }
            }
        }
    }

    // ����� ���� Ȯ��
    private bool IsMutuallyConnected(TileRay tileRay, GameObject targetObject)
    {
        List<GameObject> hitObjects = tileRay.GetHitObject();
        return hitObjects.Contains(targetObject);
    }

    // ����� ������Ʈ�� ��� Ÿ�� ������Ʈ�� ��ġ�ϴ��� Ȯ��
    private bool AreAllTargetsConnected(List<GameObject> connectedObjects)
    {
        foreach (var target in targetObjects)
        {
            if (!connectedObjects.Contains(target))
            {
                //Debug.Log("Not all target objects are connected.");
                isComplete = false;
                return false; // Ÿ�� ������Ʈ �� ������� ���� ���� ����
            }
        }

        Debug.Log("All target objects are successfully connected!");
        this.connectedObjects = connectedObjects;
        isComplete = true;
        return true; // ��� Ÿ�� ������Ʈ�� �����
    }
}
