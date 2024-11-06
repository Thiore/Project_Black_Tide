using UnityEngine;
using UnityEngine.InputSystem; // Input System ���ӽ����̽� �߰�

public class Connect : MonoBehaviour
{
    private Camera mainCamera;
    private LineRenderer lineRenderer; // LineRenderer �߰�
    private bool isDragging = false; // �巡�� �� ����
    private Vector3 startTouchPosition; // ��ġ ���� ��ġ

    void Start()
    {
        mainCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>(); // LineRenderer ������Ʈ ��������
        lineRenderer.positionCount = 0; // �ʱ⿣ ���� ������ ����
        lineRenderer.enabled = false; // ó���� LineRenderer ��Ȱ��ȭ
    }

    void Update()
    {
        // ��ġ �Է� ó��
        if (Touchscreen.current != null)
        {
            var primaryTouch = Touchscreen.current.primaryTouch;

            // ��ġ�� ���۵� ��
            if (primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                Vector2 touchPosition = primaryTouch.position.ReadValue();
                CheckForObject(touchPosition);
                startTouchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane));
                isDragging = true;
                lineRenderer.positionCount = 1; // ���� ������ ���� 1�� ���� (������)
                lineRenderer.SetPosition(0, startTouchPosition); // ������ ����
                // lineRenderer.enabled = true; // �巡�װ� ���۵Ǹ� LineRenderer Ȱ��ȭ
            }

            // ��ġ�� �̵��� ��
            if (isDragging && primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                Vector2 touchPosition = primaryTouch.position.ReadValue();
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane));
                worldPosition.x = startTouchPosition.x; // X���� ���� ��ġ�� ����
                lineRenderer.positionCount = 2; // ���� ������ ���� 2�� ���� (�������� ����)
                lineRenderer.SetPosition(1, worldPosition); // Y�࿡ �°Ը� ���� ������Ʈ
            }

            // ��ġ�� ���� ��
            if (primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                isDragging = false;
                lineRenderer.enabled = false; // �巡�� ������ LineRenderer ��Ȱ��ȭ
            }
        }

        // ���콺 �Է� ó��
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                CheckForObject(mousePosition);
                startTouchPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));
                isDragging = true;
                lineRenderer.positionCount = 1; // ���� ������ ���� 1�� ���� (������)
                lineRenderer.SetPosition(0, startTouchPosition); // ������ ����
                lineRenderer.enabled = true; // �巡�װ� ���۵Ǹ� LineRenderer Ȱ��ȭ
            }

            // ���콺 �巡�� ���� ��
            if (isDragging && Mouse.current.leftButton.isPressed)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));
                worldPosition.x = startTouchPosition.x; // X���� ���� ��ġ�� ����
                lineRenderer.positionCount = 2; // ���� ������ ���� 2�� ���� (�������� ����)
                lineRenderer.SetPosition(1, worldPosition); // Y�࿡ �°Ը� ���� ������Ʈ
            }

            // ���콺 ��ư�� �������� ��
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                isDragging = false;
                lineRenderer.enabled = false; // �巡�� ������ LineRenderer ��Ȱ��ȭ
            }
        }
    }

    void CheckForObject(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Connect"))
            {
                Debug.Log("��ġ �Ϸ�!");
            }
        }
    }
}
