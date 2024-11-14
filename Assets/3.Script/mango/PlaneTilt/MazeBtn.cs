using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBtn : MonoBehaviour
{
    public GameObject plane;

    private const string BodySensorsPermission = "android.permission.BODY_SENSORS";

    public void enablePlane()
    {
        plane.SetActive(true);
        plane.GetComponent<PlaneTiltController>().GetAccelerometer();

        // ������ �ִ��� Ȯ��
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(BodySensorsPermission))
        {
            Debug.Log("BODY_SENSORS ������ �����ϴ�. ��û ��...");
            UnityEngine.Android.Permission.RequestUserPermission(BodySensorsPermission);
        }
        else
        {
            Debug.Log("BODY_SENSORS ������ �̹� �ο��Ǿ����ϴ�.");
        }
    }
    
}