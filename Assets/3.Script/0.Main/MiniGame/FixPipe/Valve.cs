using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Valve : MonoBehaviour, ITouchable
{
    [SerializeField] private FixPipeGameManager pipegameManager;
    //0 90(left) -180(up) -90
    [SerializeField] private bool ispipecheck; // 파이프 연결 확인해야하는경우 true

    private Vector3 rotationAxis = Vector3.forward; // 회전 축
    private float duration = 0.6f; // N초 동안 회전
    private float angle = 90f; // 90도 회전
    private bool isRotating;

    [SerializeField] private Valve nextValve;
    [SerializeField] private Pipe directionPipe;
    public Pipe DirectionPipe { get => directionPipe; }

    // Ray 관련 변수
    [SerializeField] private float rayDistance = 2.5f; // Ray 거리
    [SerializeField] LayerMask targetLayer;

    [SerializeField] private GameObject parentpipe;

    private void Awake()
    {
        parentpipe = gameObject.transform.parent.parent.gameObject;
    }

    private void Start()
    {
        ShootRay();
    }

    private void Update()
    {
        Vector3 direction = transform.right;
        Debug.DrawRay(parentpipe.transform.position, direction * rayDistance, Color.red); // 중심선
    }

    public void OnTouchEnd(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, TouchManager.Instance.getTouchDistance, TouchManager.Instance.getTouchableLayer))
        {
            if (hit.collider.gameObject.Equals(gameObject) && !isRotating)
            {
                RotateValve();
            }
        }
    }

    public void RotateValve()
    {
        StartCoroutine(RotataAndRay());
        pipegameManager.FindPath();
    }

    private IEnumerator RotataAndRay()
    {
        // 회전 코루틴 실행 및 완료 대기
        yield return StartCoroutine(RotateValve_co());

        // 코루틴이 끝난 후 Ray 실행
        pipegameManager.FindPath();
    }

    private IEnumerator RotateValve_co()
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(rotationAxis * angle);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.fixedDeltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        transform.rotation = endRotation; // 정확한 최종 값 설정
        isRotating = false;
    }

    public void ShootRay()
    {
        if(nextValve != null) nextValve = null;
        if(directionPipe != null) directionPipe = null;
        // Ray 방향을 객체의 로컬 방향으로 설정
        Vector3 direction = transform.right; // Raycast 방향

        // Ray 생성 및 발사
        Ray ray = new Ray(parentpipe.transform.position, direction);

        RaycastHit[] hits = Physics.RaycastAll(parentpipe.transform.position, direction, rayDistance, targetLayer);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Pipe pipe))
            {
                if (directionPipe != null)
                {
                    float pipesqrDistance = (parentpipe.transform.position - pipe.transform.position).sqrMagnitude;
                    float directsqrDistance1 = (parentpipe.transform.position - directionPipe.transform.position).sqrMagnitude;
                    if (pipesqrDistance < directsqrDistance1)
                    {
                        directionPipe = pipe;
                    }
                }
                else
                {
                    directionPipe = pipe;
                }
            }

            if (hit.collider.gameObject.TryGetComponent(out Valve valve))
            {
                if(nextValve != null)
                {
                    var newvaledistran = Vector3.Distance(transform.position, valve.transform.position);
                    var nextdistran = Vector3.Distance(transform.position, nextValve.transform.position);
                    //float valvesqrDistance = (transform.position - valve.transform.position).sqrMagnitude;
                    //float nextvalvesqrDistance1 = (transform.position - nextValve.transform.position).sqrMagnitude;
                    if (newvaledistran < nextdistran)
                    {
                        nextValve = valve;
                        Debug.Log("바뀌었고");
                    }
                }
                else
                {
                    nextValve = valve;
                }
            }
        }

        // 디버그용 SphereCast 시각화
        Debug.DrawRay(parentpipe.transform.position, direction * 2f, Color.red); // 중심선

    }



    public void OnTouchHold(Vector2 position)
    {

    }

    public void OnTouchStarted(Vector2 position)
    {

    }

    public Valve FindNextValve()
    {
        return nextValve;
    }

}