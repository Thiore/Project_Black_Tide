using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickManager : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private RectTransform handle;
    [SerializeField] private RectTransform leftArea;
    [SerializeField] private RectTransform joystickArea;

    public LayerMask touchableLayer;
    private Canvas canvas;  // ĵ���� ����
    private Vector2 joystickCenter; // ���̽�ƽ �߽� ��ǥ
    public float maxDistance = 70f; // �ڵ��� ������ �� �ִ� �ִ� �Ÿ�
    private bool joystickActive = false; // ���̽�ƽ Ȱ��ȭ ����

    private void Start()
    {
        // ĵ���� ���� ��������
        canvas = GetComponentInParent<Canvas>();

        // ���̽�ƽ�� �ʱ⿡�� ��Ȱ��ȭ
        joystick.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            
            if (joystickActive) HandleMove(); else
            {
                // ��ġ ���¿� ���� ó��
                HandleTouchBegan(touchPosition);
            }
        }

        HandleTouchEnded();
    }

    // ��ġ ���� �� ȣ��
    private void HandleTouchBegan(Vector2 touchPosition)
    {
        if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(leftArea, touchPosition, canvas.worldCamera))
            {
                HandleLeftAreaTouch(touchPosition);
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(joystickArea, touchPosition, canvas.worldCamera))
            {
                ActivateJoystick(touchPosition, true); // ���̽�ƽ Ȱ��ȭ
                joystickCenter = joystick.GetComponent<RectTransform>().anchoredPosition; // ���̽�ƽ �߽� ��ǥ ����
                joystickActive = true; // ���̽�ƽ Ȱ��ȭ �÷��� ����
            }
        }
    }

    // ��ġ �̵� �� ȣ�� (�հ��� �̵��� ���� �ڵ��� �ٷ� ����)
    private void HandleMove()
    {
        if (joystickActive && Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            // ���� ��ġ�� ��ġ�� �����ͼ� ���̽�ƽ �߽ɿ��� �ڵ��� �̵���Ŵ
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            MoveHandleWithFinger(touchPosition); // ��ġ ��ġ�� ���� �ڵ� �̵�
        }
    }

    // �հ��� �����ӿ� ���� �ڵ��� �̵���Ű�� �޼���
    private void MoveHandleWithFinger(Vector2 touchPosition)
    {
        // �ڵ��� ���̽�ƽ �߽����κ��� �̵��� ������ ���
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystick.GetComponent<RectTransform>(),
            touchPosition,
            canvas.worldCamera,
            out localPoint
        );

        Vector2 offset = localPoint - joystickCenter;

        // �ִ� �ݰ� ������ �ڵ� �̵� ����
        if (offset.magnitude > maxDistance)
        {
            offset = offset.normalized * maxDistance;
        }

        // �ڵ� ��ġ ������Ʈ
        handle.anchoredPosition = offset;
    }

    // ��ġ�� ����Ǿ��� �� ȣ��
    private void HandleTouchEnded()
    {
        if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            ActivateJoystick(Vector2.zero, false); // ���̽�ƽ ��Ȱ��ȭ
            handle.anchoredPosition = Vector2.zero; // �ڵ��� �߾����� ����
            joystickActive = false; // ���̽�ƽ Ȱ��ȭ �÷��� ����
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
            if (hit.collider.CompareTag("Touchable Object"))
            {
                return;
            }
        }

        // ���̽�ƽ Ȱ��ȭ
        ActivateJoystick(touchPosition, true);
    }

    // ���̽�ƽ Ȱ��ȭ/��Ȱ��ȭ
    private void ActivateJoystick(Vector2 touchPosition, bool isActive)
    {
        if (isActive)
        {
            Vector2 parentSize = joystick.GetComponentInParent<RectTransform>().rect.size;

            // ��ġ�� ��ġ�� ĵ���� ��ǥ�� ��ȯ
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                touchPosition,
                canvas.worldCamera,
                out localPoint
            );

            // ���̽�ƽ�� ��ġ�� ��ġ�� ��ġ
            joystick.GetComponent<RectTransform>().anchoredPosition = localPoint; 

            joystickCenter = localPoint; // ���̽�ƽ �߽� ��ǥ ����
            joystick.SetActive(true);
        }
        else
        {
            joystick.SetActive(false); // ���̽�ƽ ��Ȱ��ȭ
        }
    }
}
