using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorrectCheck : MonoBehaviour
{
    public GameObject image;
    public string targetTag = "TargetObject"; // ������ �±� �̸�
    public float rayDistance = 1f;             // Ray�� ����

    private void Awake()
    {
        image.SetActive(false);
    }

    [SerializeField]
    private Vector3[] rayOffsets = new Vector3[4]  // Ray ���� ��ġ�� ������ �迭�� �ν����Ϳ��� ���� �����ϰ� ��
    {
        new Vector3(0.25f, 0.2f, 0),   // �⺻ ��
        new Vector3(-0.25f, 0.2f, 0),
        new Vector3(0.25f, -0.2f, 0),
        new Vector3(-0.25f, -0.2f, 0)
    };

    public bool CheckAllRays()
    {
        foreach (Vector3 offset in rayOffsets)
        {
            // Quad Object �������� Ray�� �߻��� ���� ��ġ ����
            Vector3 rayOrigin = transform.position + offset;

            // y �������� Ray �߻�
            if (Physics.Raycast(rayOrigin, transform.up, out RaycastHit hit, rayDistance))
            {
                // �浹�� ������Ʈ�� �±װ� targetTag�� ��ġ���� ������ false ��ȯ
                if (hit.collider.CompareTag(targetTag) == false)
                {
                    Debug.Log($"Ray from {rayOrigin} did not hit target tag.");
                    return false;
                }
            }
            else
            {
                // Ray�� �ƹ� ������Ʈ�͵� �浹���� ������ false ��ȯ
                Debug.Log($"Ray from {rayOrigin} did not hit anything.");
                return false;
            }
        }

        // ��� Ray�� ������ �±��� ������Ʈ�� �浹�ϸ� true ��ȯ
        Debug.Log("All rays hit the correct target.");
        Debug.Log("Game Win");
        image.SetActive(true);
        Invoke("GameEnd", 3f);
        return true;
    }

    private void OnDrawGizmos()
    {
        // Gizmos�� ����� Scene View���� Ray�� �ð������� Ȯ��
        Gizmos.color = Color.green;
        foreach (Vector3 offset in rayOffsets)
        {
            Vector3 rayOrigin = transform.position + offset;
            Gizmos.DrawRay(rayOrigin, transform.up * rayDistance);
        }
    }
    private void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}
