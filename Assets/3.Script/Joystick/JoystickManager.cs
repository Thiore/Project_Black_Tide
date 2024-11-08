using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickManager : MonoBehaviour, ITouchable
{
    [SerializeField] private RectTransform joystick;
    [SerializeField] private RectTransform handle;
    private Canvas canvas;
    

    private Vector2 joystickCenter; // ���̽�ƽ �߽� ��ǥ
    public float maxDistance = 70f; // �ڵ��� ������ �� �ִ� �ִ� �Ÿ�


    private void Start()
    {
        TryGetComponent(out canvas);
        TouchManager.Instance.OnMoveStarted += OnTouchStarted;
        TouchManager.Instance.OnMoveHold += OnTouchHold;
        TouchManager.Instance.OnMoveEnd += OnTouchEnd;
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
    /// �� ������ ��ġ ��ġ�� ���� ���� �����е� �ڵ��� ��ǥ ����
    /// </summary>
    /// <param name="���� ��ġ�ǰ� �ִ� ��ġ ��ǥ"></param>
    private void MoveHandle(Vector2 touchPosition)
    {
        // ��ġ�� ��ũ�� ��ǥ�� ���̽�ƽ ��ǥ�� ��ȯ
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystick,
            touchPosition,
            null,
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
        handle.anchoredPosition = offset;
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
                canvas.transform as RectTransform,
                touchPosition,
                null,
                out localPoint
            );

            // ���̽�ƽ�� ��ġ�� ��ġ�� ��ġ
            joystick.anchoredPosition = localPoint; 
           
            joystickCenter = localPoint; // ���̽�ƽ �߽� ��ǥ ����
            joystick.gameObject.SetActive(true);
        }
        else
        {
            joystick.gameObject.SetActive(false); // ���̽�ƽ ��Ȱ��ȭ
        }
    }

    public void OnTouchStarted(Vector2 position)
    {
        ActivateJoystick(position, true); // ���̽�ƽ Ȱ��ȭ
        joystickCenter = joystick.anchoredPosition; // ���̽�ƽ �߽� ��ǥ ����
    }
    
    public void OnTouchHold(Vector2 position)
    {
        // ���� ��ġ�� ��ġ�� �����ͼ� ���̽�ƽ �߽ɿ��� �ڵ��� �̵���Ŵ
        MoveHandle(position); // ��ġ ��ġ�� ���� �ڵ� �̵�
    }

    public void OnTouchEnd(Vector2 position)
    {
        handle.anchoredPosition = Vector2.zero; // �ڵ��� �߾����� ����
        ActivateJoystick(Vector2.zero, false); // ���̽�ƽ ��Ȱ��ȭ
    }
}
