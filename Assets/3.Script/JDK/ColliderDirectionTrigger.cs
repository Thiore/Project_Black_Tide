using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ColliderDirectionTrigger : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Camera mainCamera;

    // Input Action
    public InputAction clickAction;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        mainCamera = Camera.main;

        // Input Action Ȱ��ȭ
        clickAction.Enable();
        clickAction.performed += OnClick;
    }

    private void OnDestroy()
    {
        // Input Action ��Ȱ��ȭ
        clickAction.performed -= OnClick;
        clickAction.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        // ���콺 �Ǵ� ��ġ ��ġ ��������
        Vector2 screenPosition = Pointer.current.position.ReadValue();

        // ��ũ�� ��ǥ�� ���� Ray�� ��ȯ
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        // Raycast�� Collider �浹 ����
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == boxCollider)
            {
                CheckDirection(hit.point);
            }
        }
    }

    private void CheckDirection(Vector3 hitPoint)
    {
        // BoxCollider�� �߽ɰ� ũ��
        Vector3 center = boxCollider.bounds.center;
        Vector3 extents = boxCollider.bounds.extents;

        // hitPoint�� �߽��� ���Ͽ� ��� �鿡 ������� Ȯ��
        if (Mathf.Abs(hitPoint.x - (center.x + extents.x)) < 0.1f)
        {
            TriggerRightEvent();
        }
        else if (Mathf.Abs(hitPoint.x - (center.x - extents.x)) < 0.1f)
        {
            TriggerLeftEvent();
        }
        else if (Mathf.Abs(hitPoint.y - (center.y + extents.y)) < 0.1f)
        {
            TriggerTopEvent();
        }
        else if (Mathf.Abs(hitPoint.y - (center.y - extents.y)) < 0.1f)
        {
            TriggerBottomEvent();
        }
    }

    private void TriggerRightEvent()
    {
        Debug.Log("Right side hit!");
        // ������ �̺�Ʈ ���� �ڵ�
    }

    private void TriggerLeftEvent()
    {
        Debug.Log("Left side hit!");
        // ���� �̺�Ʈ ���� �ڵ�
    }

    private void TriggerTopEvent()
    {
        Debug.Log("Top side hit!");
        // ���� �̺�Ʈ ���� �ڵ�
    }

    private void TriggerBottomEvent()
    {
        Debug.Log("Bottom side hit!");
        // �Ʒ��� �̺�Ʈ ���� �ڵ�
    }
}