using UnityEngine;

public class LineCollider : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer = null; // LineRenderer 컴포넌트 참조

    private BoxCollider boxCollider; // BoxCollider 컴포넌트 참조

    private void Awake()
    {
        // BoxCollider 컴포넌트 가져오기
        boxCollider = GetComponent<BoxCollider>();

        // BoxCollider가 없으면 자동으로 추가
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }
    }

    private void Update()
    {
        // LineRenderer가 두 개 이상의 점을 가지고 있는지 확인
        if (lineRenderer.positionCount >= 2)
        {
            // LineRenderer의 첫 번째와 두 번째 점의 월드 좌표 가져오기
            Vector3 startWorld = lineRenderer.GetPosition(0);
            Vector3 endWorld = lineRenderer.GetPosition(1);

            // 두 점의 중간 위치를 BoxCollider의 중심으로 설정
            Vector3 centerWorld = (startWorld + endWorld) / 2;
            transform.position = centerWorld;

            // 두 점 간의 방향 벡터 계산
            Vector3 direction = endWorld - startWorld;

            // 방향 벡터가 유효한 경우에만 회전 설정
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction); // 방향에 따라 회전 설정
            }
            else
            {
                transform.rotation = Quaternion.identity; // 방향이 없을 경우 기본 회전으로 설정
            }

            // BoxCollider의 크기를 LineRenderer의 길이에 맞게 설정
            float length = direction.magnitude; // 두 점 간 거리 계산
            boxCollider.size = new Vector3(0.1f, 0.1f, length); // Z축 기준으로 길이 설정
        }
    }
}