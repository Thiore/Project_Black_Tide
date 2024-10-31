using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputData
{
    public InputAction action { get; private set; } // ��ġ�� ��ġ���� �߻� �� �׼�
    public Vector2 startValue { get; private set; } // ��ġ�� ���۵� ��ġ
    public Vector2 value { get; private set; } // ��ġ �� ���� ��ġ
    public bool isTouch { get; private set; } // ��ġ ������ Ȯ��

    public InputData(InputAction action)
    {
        this.action = action;
        this.startValue = Vector2.zero;
        this.value = Vector2.zero;
        this.isTouch = false;
    }

    
    public void SetStartTouchPos(int touchId, Vector2 touchPos)
    {
        startValue = touchPos;
        isTouch = true;
    }
    public void SetCurrentPos(Vector2 touchPos)
    {
        value = touchPos;
    }
    public void ResetData()
    {
        startValue = Vector2.zero;
        value = Vector2.zero;
        isTouch = false;
    }
}
public class InputManager : MonoBehaviour
{
    private InputManager instance = null;
    public static InputManager Instance { get; private set; }

    private enum etouchState
    {
        Normal = 0,
        Player,
        UI,
        Object
    }
    
    [SerializeField] private RectTransform joystickArea; // Joystick Area
    [SerializeField] private LayerMask touchableUILayer; // ��ġ ������ UI ���̾�
    [SerializeField] private LayerMask touchableObjectLayer; // ��ġ ������ ������Ʈ ���̾�
    
    [SerializeField] private InputActionAsset playerInput;

    public Dictionary<int, InputData> activeActionDic { get; private set; }
    private GraphicRaycaster graphicRaycast;
    private etouchState touchState;
    //������ Ŭ���� ����
    public InputData moveData { get; private set; }
    public InputData lookData { get; private set; }
    public InputData UI0Data{ get; private set; }
    public InputData UI1Data{ get; private set; }
    public InputData objectData { get; private set; }

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
        FindAction();
        
        AddEventPerformedAction();
        
        SetActionDisable(); // ��� �׼� Disable
        
        activeActionDic = new Dictionary<int, InputData>();
        touchState = etouchState.Normal;
    }
    
    private void Update()
    {
        //if (Touchscreen.current == null) return;
        Debug.Log(activeActionDic.Count);
        foreach (var touch in Touchscreen.current.touches)
        {
            int touchId = touch.touchId.ReadValue() - 1;
            Vector2 touchPos = touch.position.ReadValue();

            if(touch.press.isPressed && !activeActionDic.ContainsKey(touchId))
            {
                if (IsTouchOnUI(touchPos, touchId)
                    && touchId<2
                    && (touchState.Equals(etouchState.Normal)||touchState.Equals(etouchState.UI)))
                {
                    // UI �׼� ����
                    touchState = etouchState.UI;
                    
                    switch(touchId)
                    {
                        case 0:
                            BindAction(touchId, UI0Data, touchPos);
                            break;
                        case 1:
                            BindAction(touchId, UI1Data, touchPos);
                            break;
                    }
                }
                else if (IsTouchOnJoystickArea(touchPos)
                        && (touchState.Equals(etouchState.Normal) 
                        || touchState.Equals(etouchState.Player)))
                {
                    touchState = etouchState.Player;

                    BindAction(touchId, moveData, touchPos);


                }
                else if (IsTouchableObjectAtPosition(touchPos)&&touchState.Equals(etouchState.Normal))
                {
                    touchState = etouchState.Object;
                    BindAction(touchId, objectData, touchPos);
                }
                else if(touchState.Equals(etouchState.Normal)|| touchState.Equals(etouchState.Player))
                {
                    if (IsTouchOnLeftScreen(touchPos) && !moveData.isTouch)
                    {
                        if (touchState.Equals(etouchState.Normal))
                            touchState = etouchState.Player;

                        BindAction(touchId, moveData, touchPos);
                    }
                    else if (IsTouchOnRightScreen(touchPos))
                    {
                        if (touchState.Equals(etouchState.Normal))
                            touchState = etouchState.Player;

                        BindAction(touchId, lookData, touchPos);
                    }
                }
            }
            

            if (!touch.press.isPressed && activeActionDic.ContainsKey(touchId))
            {
                RomoveBindAction(touchId);
            }

        }
        
    }

    private bool IsTouchOnUI(Vector2 touchPosition, int touchId)
    {
        // UI ��ġ �˻�


        return EventSystem.current.IsPointerOverGameObject(touchId);
    }

    private bool IsTouchOnJoystickArea(Vector2 touchPosition)
    {
        // JoystickArea ��ġ �˻�
        return RectTransformUtility.RectangleContainsScreenPoint(joystickArea, touchPosition);
    }

    // ��ġ�� ���� "Touchable Object"�� �ִ��� Ȯ���ϴ� �޼���
    private bool IsTouchableObjectAtPosition(Vector2 touchPosition)
    {
        if (touchPosition.Equals(Vector2.zero)) return false;

        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchableObjectLayer))
        {
            // "Touchable Object" �±׸� ���� ������Ʈ�� �ִ��� Ȯ��
            if (hit.collider.CompareTag("touchableobject"))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsTouchOnLeftScreen(Vector2 touchPosition)
    {
        // ȭ���� ���� ���� ��ġ �˻�
        return touchPosition.x < Screen.width / 2;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="touchPosition">adfs</param>
    /// <returns></returns>
    private bool IsTouchOnRightScreen(Vector2 touchPosition)
    {
        // ȭ���� ������ ���� ��ġ �˻�
        return touchPosition.x >= Screen.width / 2;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveData.SetCurrentPos(context.ReadValue<Vector2>());
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookData.SetCurrentPos(context.ReadValue<Vector2>());
    }
    private void OnUI0(InputAction.CallbackContext context)
    {
        UI0Data.SetCurrentPos(context.ReadValue<Vector2>());
    }
    private void OnUI1(InputAction.CallbackContext context)
    {
        UI1Data.SetCurrentPos(context.ReadValue<Vector2>());
    }
    private void OnObject(InputAction.CallbackContext context)
    {
        objectData.SetCurrentPos(context.ReadValue<Vector2>());
    }

    private void BindAction(int id, InputData data, Vector2 touchPos)
    {
        if(!activeActionDic.ContainsValue(data))
        {
            activeActionDic[id] = data;
                data.action.AddBinding($"<Touchscreen>/touch{id}/position");
                data.SetStartTouchPos(id, touchPos);
            
            data.action.Enable();            
        }
        
    }
    private void RomoveBindAction(int id)
    {
        if(activeActionDic.TryGetValue(id, out var data))
        {
            activeActionDic.Remove(id);
            //resetAction[id] = false;
            data.action.Disable();
            data.action.RemoveAllBindingOverrides();
            data.ResetData();

            


        }
        if(activeActionDic.Count.Equals(0))
        {
            touchState = etouchState.Normal;
        }
    }
    /// <summary>
    /// Action �ʱ�ȭ
    /// </summary>
    private void FindAction()
    {
        InputActionMap playerMap = playerInput.FindActionMap("Player");
        moveData = new InputData(playerMap.FindAction("Move"));
        lookData = new InputData(playerMap.FindAction("Look"));

        InputActionMap UIMap = playerInput.FindActionMap("UI");
        UI0Data = new InputData(UIMap.FindAction("UI0"));
        UI1Data = new InputData(UIMap.FindAction("UI1"));

        InputActionMap ObjMap = playerInput.FindActionMap("Interaction");
        objectData = new InputData(ObjMap.FindAction("Object"));
    }
    /// <summary>
    /// �� �������� Action�� �̺�Ʈ �߰�
    /// </summary>
    private void AddEventPerformedAction()
    {
        moveData.action.performed += OnMove;
        lookData.action.performed += OnLook;
        UI0Data.action.performed += OnUI0;
        UI1Data.action.performed += OnUI1;
        objectData.action.performed += OnObject;
    }
    /// <summary>
    /// �ʱ�ȭ �� �׼� ��Ȱ��ȭ
    /// </summary>
    private void SetActionDisable()
    {
        moveData.action.Disable();
        lookData.action.Disable();
        UI0Data.action.Disable();
        UI1Data.action.Disable();
        objectData.action.Disable();
    }
}
