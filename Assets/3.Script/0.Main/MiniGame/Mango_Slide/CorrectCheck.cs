//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class CorrectCheck : MonoBehaviour
//{
//    public GameObject image;
    
//    private float rayDistance = 1f; // Ray의 길이

//    private void Awake()
//    {
//        image.SetActive(false);
//    }

//    [SerializeField]
//    private Vector3[] rayOffsets;// Ray 시작 위치의 오프셋 배열을 인스펙터에서 설정 가능하게 함
    

//    public bool CheckAllRays(GameObject checkObj)
//    {
//        foreach (Vector3 offset in rayOffsets)
//        {
//            // Quad Object 기준으로 Ray를 발사할 시작 위치 설정
//            Vector3 rayOrigin = transform.position + offset;

//            // y 방향으로 Ray 발사
//            if (Physics.Raycast(rayOrigin, transform.up, out RaycastHit hit, rayDistance))
//            {
//                // 충돌한 오브젝트와 일치하지 않으면 false 반환
//                if (!hit.collider.gameObject.Equals(checkObj))
//                {
//                    Debug.Log($"Ray from {rayOrigin} did not hit target tag.");
//                    return false;
//                }
//            }
//            else
//            {
//                // Ray가 아무 오브젝트와도 충돌하지 않으면 false 반환
//                Debug.Log($"Ray from {rayOrigin} did not hit anything.");
//                return false;
//            }
//        }

//        // 모든 Ray가 지정된 태그의 오브젝트와 충돌하면 true 반환
//        Debug.Log("All rays hit the correct target.");
//        Debug.Log("Game Win");
//        image.SetActive(true);
//        Invoke("GameEnd", 3f);
//        return true;
//    }
//#if UNITY_EDITOR
//    private void OnDrawGizmos()
//    {

//        // Gizmos를 사용해 Scene View에서 Ray를 시각적으로 확인
//        Gizmos.color = Color.green;
//        foreach (Vector3 offset in rayOffsets)
//        {
//            Vector3 rayOrigin = transform.position + offset;
//            Gizmos.DrawRay(rayOrigin, transform.up * rayDistance);
//        }
//    }
//#endif
//    private void GameEnd()
//    {
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#else
//    Application.Quit();
//#endif
//    }

//}
