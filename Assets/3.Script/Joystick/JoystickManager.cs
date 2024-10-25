using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickManager : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private RectTransform leftArea;
    [SerializeField] private RectTransform joystickArea;

    public LayerMask touchableLayer;
    private Canvas canvas;  // ĵ���� ����

    private void Start()
    {
        // ĵ���� ã�� (�θ𿡼� ĵ���� ������Ʈ�� ã���ϴ�)
        canvas = GetComponentInParent<Canvas>();

        // ���̽�ƽ�� �ʱ⿡�� ��Ȱ��ȭ
        joystick.SetActive(false);
    }

    private void Update()
    {
        // ��ġ �Է��� ���� �� ó��
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            // ��ġ�� ���۵Ǿ��� ��
            if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                // LeftArea�� ��ġ�ߴ��� Ȯ��
                if (RectTransformUtility.RectangleContainsScreenPoint(leftArea, touchPosition, canvas.worldCamera))
                {
                    HandleLeftAreaTouch(touchPosition);
                }
                // JoystickArea�� ��ġ�ߴ��� Ȯ��
                else if (RectTransformUtility.RectangleContainsScreenPoint(joystickArea, touchPosition, canvas.worldCamera))
                {
                    ActivateJoystick(touchPosition); // �ٷ� ���̽�ƽ Ȱ��ȭ
                }
            }

            // ��ġ�� ������ �� ���̽�ƽ ��Ȱ��ȭ
            if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                ActivateJoystick(Vector2.zero, false); // ���̽�ƽ ��Ȱ��ȭ
            }
        }
    }

    // LeftArea ��ġ ó��
    private void HandleLeftAreaTouch(Vector2 touchPosition)
    {
        // ��ġ�� ��ġ�� "Touchable Object" �±׸� ���� ������Ʈ�� �ִ��� Ȯ��
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchableLayer))
        {
            // "Touchable Object" �±׸� ���� ������Ʈ�� ������ �ƹ� ���۵� ���� ����
            if (hit.collider.CompareTag("Touchable Object"))
            {
                return;
            }
        }

        // ������ ���̽�ƽ Ȱ��ȭ
        ActivateJoystick(touchPosition, true);
    }

    // ���̽�ƽ Ȱ��ȭ/��Ȱ��ȭ
    private void ActivateJoystick(Vector2 touchPosition, bool isActive = true)
    {
        if (isActive)
        {
            // ��ġ�� ��ġ�� ĵ���� ��ǥ�� ��ȯ
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,  // ĵ������ RectTransform
                touchPosition,                    // ��ġ�� ��ũ�� ��ǥ
                canvas.worldCamera,// ���� ī�޶� (Overlay ��忡���� null ����)
                out localPoint);                  // ��ȯ�� ���� ��ǥ ��ȯ

            // ���̽�ƽ�� ��ġ�� ��ġ�� ��ġ (anchoredPosition ���)
            // ���̽�ƽ ��Ŀ���� ���� ��ġ ���� ������ �ǽ�
            joystick.GetComponent<RectTransform>().anchoredPosition = localPoint + new Vector2(1200,450);
            joystick.SetActive(true);
        }
        else
        {
            joystick.SetActive(false); // ���̽�ƽ ��Ȱ��ȭ
        }
    }
}
