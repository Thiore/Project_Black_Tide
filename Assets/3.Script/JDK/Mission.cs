using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/* public enum ConnectionColor
{
    Red,
    Blue,
    Gray,
    Pink,
    Orange,
    Yellow,
    Sky_Blue,
} */

public class Mission : MonoBehaviour
{
    [Header("Line ����ġ")]
    [SerializeField] private GameObject obj = null;
    [SerializeField] private LineRenderer lr = null;

    // [Header("Box ����ġ")]
    // [SerializeField] private Transform[] box = null;

    private bool ClickState = false;

    // [SerializeField] private ConnectionColor color;

    public void ResetButton()
    {
        Debug.Log("����!");
    }

    public void TutorialButton()
    {
        Debug.Log("Ʃ�丮��!");
    }

    public void CloseButton()
    {
        Debug.Log("������!");
    }

    public void ChangeColor(Material color)
    {
        lr.material = color;
    }

    public void StartDrag(GameObject pos)
    {
        if (!lr.enabled)
        {
            lr.gameObject.transform.SetParent(pos.transform);

            lr.gameObject.transform.localPosition = new Vector3(0.6f, 0, 0);

            Debug.Log("Ȱ��ȭ!");

            lr.enabled = true;
        }
    }

    public void EndDrag()
    {
        Debug.Log("��Ȱ��ȭ!");

        lr.enabled = false;
    }

    public void Box()
    {
        int count = lr.positionCount;

        count += 1;

        // �߰� ���� �ʿ�...
    }
}