using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputSystemObjectClicker : MonoBehaviour
{
    public Camera mainCamera; // ���� ī�޶� ����

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    // Pointer �Է� ó�� (���콺 Ŭ�� �Ǵ� ��ġ)
    public void OnPointerDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 screenPosition = Pointer.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log($"Clicked on: {hit.collider.gameObject.name}");
            }
        }
    }
}
