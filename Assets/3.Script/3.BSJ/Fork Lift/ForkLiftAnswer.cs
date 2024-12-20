using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ForkLiftAnswer : MonoBehaviour
{
    [SerializeField] private List<ForkLiftCollect> allCollectZones; //모든 정답 구역
    [SerializeField] private CinemachineVirtualCamera cam_2D; //파레트를 옮기는 2D 카메라
    [SerializeField] private CinemachineVirtualCamera cam_3D; //정답 구역 앞 카메라
    [SerializeField] private CinemachineBrain cinemachineBrain; //Blend 변경을 위한 참조
    [SerializeField] private GameObject cutScene; //정답일 때, 컷씬 재생
    [SerializeField] private GameObject originalobj; //퍼즐 완료 후, 미리 배치 해놨던 탑

    private void Start()
    {
        //정답 상태 초기화
        foreach(var zone in allCollectZones)
        {
            zone.isCollect = false;
        }
        
        
    }

    public void CheckAllZones()
    {
        foreach (var zone in allCollectZones)
        {
            //하나라도 정답이 아니라면 종료
            if (!zone.isCollect)
            {
                Debug.Log("아직 모든 구역이 정답 상태가 아님");
                return;
            }
        }
        //모든 구역이 정답 상태일 때
        Debug.Log("Finish All");
        cutScene.SetActive(true);
        Invoke("ObjTiem", 2f);
    }


    //퍼즐 카메라 바꾸기
    public void SwitchCam()
    {
        //Blend 시간 설정 (시네머신 변환 간 딜레이 없애기 위해 사용)
        cinemachineBrain.m_DefaultBlend.m_Time = 0;

        //2D가 켜져 있고 3D가 꺼져 있을 때 (정답 구역 앞 카메라로 전환 시)
        if (cam_2D.Priority > cam_3D.Priority)
        {

            // 3D 버추얼 카메라 활성화 (Perspective 모드)
            cam_2D.Priority = 0;
            cam_3D.Priority = 10;

            // 3D 카메라를 Perspective로 설정
            //cam_3D.m_Lens.Orthographic = false;

        }
        // 3D 카메라가 활성화 상태일 때 2D로 전환
        else
        {
            // 2D 버추얼 카메라 활성화 (Orthographic 모드)
            cam_3D.Priority = 0;
            cam_2D.Priority = 10;

            // 2D 카메라를 Orthographic으로 설정
            //cam_2D.m_Lens.Orthographic = true;

            Debug.Log("2D 카메라로 전환됨 (Orthographic 모드)");
        }
    }

    //활성화 / 비활성화 Invoke용 메서드
    public void ObjTiem()
    {
        Debug.Log("인보크 안 들어와유");
        gameObject.SetActive(false);
        originalobj.SetActive(true);
    }
}
