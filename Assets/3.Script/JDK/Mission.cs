using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Mission : MonoBehaviour
{
    [Header("Line ����ġ")]
    [SerializeField] private LineRenderer lr = null;

    [Header("�� ������Ƽ")]
    [SerializeField] private float LineValue = 0; // 1.2

    private bool RightState = false;
    private bool LeftState = false;
    private bool UpState = false;
    private bool DownState = false;

    public void ResetButton()
    {
        Debug.Log("����!");
    }

    public void TutorialButton()
    {
        Debug.Log("Ʃ�丮��!");
    }

    public void ChangeColor(Material color)
    {
        lr.material = color;
    }

    public void ResetLine()
    {
        EndDrag();
    }

    public void StartDrag(GameObject pos)
    {
        if (!lr.enabled)
        {
            lr.gameObject.transform.SetParent(pos.transform);

            lr.gameObject.transform.localPosition = new Vector3(0.6f, 0, 0);
            lr.gameObject.transform.localScale = new Vector3(1, 1, 1);

            lr.enabled = true;
        }
    }

    public void EndDrag()
    {
        lr.positionCount = 1;

        lr.enabled = false;
    }

    public void Line(string sentence)
    {
        int count = lr.positionCount;

        Vector3 pos = lr.GetPosition(count - 1);

        lr.positionCount = count + 1;

        switch (sentence)
        {
            case "Right":

                RightState = true;

                pos.z += LineValue;

                lr.SetPosition(count, pos);

                break;

            case "Left":

                if (RightState) return;

                LeftState = true;

                pos.z -= LineValue;

                lr.SetPosition(count, pos);

                break;

            case "Down":

                DownState = true;

                pos.y -= LineValue;

                lr.SetPosition(count, pos);

                break;
        }
    }
}