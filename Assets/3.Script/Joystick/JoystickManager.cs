using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickManager : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject handle;

    //private InputManager input;

    private Canvas canvas;  // ĵ���� ����
    private Vector2 joystickCenter; // ���̽�ƽ �߽� ��ǥ
    public float maxDistance = 70f; // �ڵ��� ������ �� �ִ� �ִ� �Ÿ�
    private bool joystickActive; // ���̽�ƽ Ȱ��ȭ ����

    private void Start()
    {
        //Input Data��������
        //input = InputManager.Instance;
        // ĵ���� ���� ��������
        TryGetComponent(out canvas);
        joystickActive = false;
    }

    private void Update()
    {
        //if(input.moveData.isTouch)
        //{
        //    // ��ġ ���¿� ���� ó��
        //    if (joystickActive)
        //    {
        //        HandleMove();
        //    }
        //    else
        //    {
        //        HandleTouchBegan(input.moveData.startValue);
        //    }
        //}
        //else
        //{
        //    if(joystickActive)
        //        HandleTouchEnded();
        //}
    }

    /// <summary>
    /// ��ġ ���� �� ȣ��
    /// </summary>
    /// <param name="touchPosition">���̽�ƽ�� Ȱ��ȭ �� ��ġ ���� ��ǥ</param>
    private void HandleTouchBegan(Vector2 touchPosition)
    {
        ActivateJoystick(touchPosition, true); // ���̽�ƽ Ȱ��ȭ
        joystick.TryGetComponent(out RectTransform rect);
        joystickCenter = rect.anchoredPosition; // ���̽�ƽ �߽� ��ǥ ����
        joystickActive = true; // ���̽�ƽ Ȱ��ȭ �÷��� ����
    }

    /// <summary>
    /// ��ġ �̵� �� ȣ��(�հ��� �̵��� ���� �ڵ��� �ٷ� ����)
    /// </summary>
    private void HandleMove()
    {
            // ���� ��ġ�� ��ġ�� �����ͼ� ���̽�ƽ �߽ɿ��� �ڵ��� �̵���Ŵ
            //MoveHandle(input.moveData.value); // ��ġ ��ġ�� ���� �ڵ� �̵�
    }

    /// <summary>
    /// �� ������ ��ġ ��ġ�� ���� ���� �����е� �ڵ��� ��ǥ ����
    /// </summary>
    /// <param name="touchPosition"></param>
    private void MoveHandle(Vector2 touchPosition)
    {
        // ��ġ�� ��ũ�� ��ǥ�� ���̽�ƽ ��ǥ�� ��ȯ
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystick.GetComponent<RectTransform>(),
            touchPosition,
            canvas.worldCamera,
            out localPoint
        );

        // �ڵ��� ���̽�ƽ �߽ɿ��� ��� �Ÿ��� ���
        Vector2 offset = localPoint;
        
        // �ڵ��� �ִ� 80f �Ÿ� �̻����� �̵����� �ʵ��� ����
        if (offset.magnitude > maxDistance)
        {
            offset = offset.normalized * maxDistance; // ������ �����ϵ�, �Ÿ��� 80f�� ����
        }
        // �ڵ��� ��ġ�� ������Ʈ (���̽�ƽ �߽ɿ��� offset��ŭ �̵�)
        handle.GetComponent<RectTransform>().anchoredPosition = offset;
    }

    /// <summary>
    /// ��ġ ����� ȣ�� �� �ʱ�ȭ
    /// </summary>
    private void HandleTouchEnded()
    {
            handle.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // �ڵ��� �߾����� ����
            ActivateJoystick(Vector2.zero, false); // ���̽�ƽ ��Ȱ��ȭ
            joystickActive = false; // ���̽�ƽ Ȱ��ȭ �÷��� ����
    }

    /// <summary>
    /// ���̽�ƽ Ȱ��ȭ/��Ȱ��ȭ
    /// </summary>
    /// <param name="touchPosition">���������е尡 Ȱ��ȭ�� ��ġ ��ġ</param>
    /// <param name="isActive">���������е��� Ȱ��ȭ ����</param>
    private void ActivateJoystick(Vector2 touchPosition, bool isActive)
    {
        if (isActive)
        {
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
            joystickActive = true;
        }
        else
        {
            joystick.SetActive(false); // ���̽�ƽ ��Ȱ��ȭ
            joystickActive = false;
        }
    }
}
