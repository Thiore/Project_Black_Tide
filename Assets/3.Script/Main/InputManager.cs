using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private InputManager instance = null;
    public static InputManager Instance { get; private set; }

    [SerializeField] private RectTransform leftArea; //// UI ������ LeftArea
    [SerializeField] private RectTransform rightArea; //// UI ������ RightArea
    [SerializeField] private RectTransform joystickArea; // Joystick Area
    [SerializeField] private LayerMask touchableLayer; // ��ġ ������ ���̾�

    private bool isPlayer = false;
    private bool isUI = false;
    private bool isPuzzle = false;

    [SerializeField] private InputActionMap playerInput;


    private InputAction moveAction;
    public Vector2 startMoveValue;
    public Vector2 moveValue;
    public bool isMove = false;
    private int moveIndex = default;

    private InputAction lookAction;
    public Vector2 startLookValue;
    public Vector2 lookValue;
    public bool isLook = false;

    //private bool isTouching = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Instance = instance;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        moveAction = playerInput.FindAction("Move");
        lookAction = playerInput.FindAction("Look");

        moveAction.started += OnMove;
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        lookAction.started += OnLook;
        lookAction.performed += OnLook;
        lookAction.canceled += OnLook;

        
        
        isPlayer = true;
    }



    private void Update()
    {
        //if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        //{
        //    Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();


        //    if (IsTouchOnJoystickArea(touchPosition))
        //    {
                
        //    }
            
                
            
        //}
       
        if(isPlayer)
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.isInProgress)
                {
                    Vector2 touchPosition = touch.position.ReadValue();

                    if (IsTouchOnLeftScreen(touchPosition))
                    {
                        moveAction.Enable();
                    }
                    else if (IsTouchOnRightScreen(touchPosition))
                    {
                        if(!lookAction.enabled)
                        lookAction.Enable();
                    }
                }
            }
        }
            



        //if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0 && Touchscreen.current.touches.Count <= 2)
        //{

        //    Vector2 touchPosition = Touchscreen.current.touches[Touchscreen.current.touches.Count - 1].position.value;

        //    //// UI ������ ��ġ�ߴ��� Ȯ��
        //    //if (IsTouchOnUI(touchPosition))
        //    //{
        //    //    // UI �׼� ����

        //    //}
        //    if (IsTouchOnJoystickArea(touchPosition))
        //    {
        //        //input
        //    }
        //    else if (IsTouchOnLeftScreen(touchPosition))
        //    {
        //        if (!moveAction.enabled) moveAction.Enable();

        //    }
        //    else if (IsTouchOnRightScreen(touchPosition))
        //    {

        //        // ������ ȭ�� ��ġ �� ī�޶� ȸ�� �׼� ����
        //        if (!lookAction.enabled) lookAction.Enable();

        //    }
        //}

    }

    private bool IsTouchOnUI(Vector2 touchPosition)
    {
        // UI ��ġ �˻�
        return EventSystem.current.IsPointerOverGameObject();
    }

    private bool IsTouchOnJoystickArea(Vector2 touchPosition)
    {
        // JoystickArea ��ġ �˻�
        return RectTransformUtility.RectangleContainsScreenPoint(joystickArea, touchPosition);
    }

    private bool IsTouchOnLeftScreen(Vector2 touchPosition)
    {
        // ȭ���� ���� ���� ��ġ �˻�
        return touchPosition.x < Screen.width / 2;
    }

    private bool IsTouchOnRightScreen(Vector2 touchPosition)
    {
        // ȭ���� ������ ���� ��ġ �˻�
        return touchPosition.x >= Screen.width / 2;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Debug.Log("start");
                startMoveValue = context.ReadValue<Vector2>();
                moveValue = context.ReadValue<Vector2>();
                isMove = true;
                break;
            case InputActionPhase.Performed:
                Debug.Log("performed");
                moveValue = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                Debug.Log("performed");
                startMoveValue = Vector2.zero;
                moveValue = Vector2.zero;
                isMove = false;
                moveAction.Disable();
                break;
        }
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Debug.Log("lstart");
                startLookValue = context.ReadValue<Vector2>();
                lookValue = context.ReadValue<Vector2>();
                isLook = true;
                break;
            case InputActionPhase.Performed:
                Debug.Log("lperformed");
                lookValue = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                Debug.Log("lperformed");
                startLookValue = Vector2.zero;
                lookValue = Vector2.zero;
                isLook = false;
                lookAction.Disable();
                break;
        }
    }
}
