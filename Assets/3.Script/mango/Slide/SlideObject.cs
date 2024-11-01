using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideObject : MonoBehaviour
{
    private Vector3 initPosition;
    private Outline outLine;
    private Collider objectCollider; // ���� ������Ʈ�� Collider
    public LayerMask overlapLayer;   // �浹�� Ȯ���� ���̾� ����

    private void Awake()
    {
        initPosition = transform.position;
        outLine = GetComponent<Outline>();
        outLine.enabled = false;

        objectCollider = GetComponent<Collider>();
        if (objectCollider == null)
        {
            Debug.LogWarning("No Collider found on this object.");
        }
    }
    

    // ���� ��ġ���� �ٸ� ������Ʈ�� ��ġ���� Ȯ���ϴ� �޼���
    public bool IsOverlapping()
    {
        return IsOverlappingAtPosition(objectCollider.bounds.center);
    }

    // Ư�� ��ġ���� �ٸ� ������Ʈ�� ��ġ���� Ȯ���ϴ� �޼���
    public bool IsOverlappingAtPosition(Vector3 position)
    {
        if (objectCollider == null) return false;

        // OverlapBox�� ����Ͽ� Ư�� ��ġ���� ��ħ ���� Ȯ��
        Collider[] overlappingColliders = Physics.OverlapBox(
            position,                      // �˻��� ��ġ
            objectCollider.bounds.extents, // Collider ũ��
            Quaternion.identity,           // ȸ�� ����
            overlapLayer                   // �浹 Ȯ���� ���̾�
        );

        // ��ģ Collider�� �ִ��� Ȯ�� (�ڱ� �ڽ��� ����)
        foreach (Collider collider in overlappingColliders)
        {
            if (collider.gameObject != gameObject)
            {
                return true; // �ٸ� ������Ʈ�� ��ħ
            }
        }
        return false; // ��ģ ������Ʈ�� ����
    }

    public void InitPosition()
    {
        transform.position = initPosition;
    }
}
