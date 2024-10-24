using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveActionToUse = null;
    [SerializeField] private float speed = 5f; // �ӵ��� �ʱ�ȭ

    private void OnEnable()
    {
        moveActionToUse.action.Enable(); // �׼� Ȱ��ȭ
    }

    private void OnDisable()
    {
        moveActionToUse.action.Disable(); // �׼� ��Ȱ��ȭ
    }

    private void Update()
    {
        Vector2 moveDirection = moveActionToUse.action.ReadValue<Vector2>();

        // �̵� ó��
        this.transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
