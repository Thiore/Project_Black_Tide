using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.EventSystems;

public class InputData
{
    public InputAction action { get; private set; } // ��ġ�� ��ġ���� �߻� �� �׼�
    public Vector2 startValue { get; private set; } // ��ġ�� ���۵� ��ġ
    public Vector2 value { get; private set; } // ��ġ �� ���� ��ġ
    public bool isTouch { get; private set; } // ��ġ ������ Ȯ��
    public bool isUsed { get; private set; }

    public InputData(InputAction action)
    {
        this.action = action;
        this.startValue = Vector2.zero;
        this.value = Vector2.zero;
        this.isTouch = false;
        this.isUsed = false;
    }

    
    public void SetStartTouchPos(int touchId, Vector2 touchPos)
    {
        startValue = touchPos;
        isTouch = true;
        isUsed = true;
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
        isUsed = false;
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
    private etouchState touchState;

    [SerializeField] private RectTransform joystickArea; // Joystick Area
    [SerializeField] private LayerMask touchableLayer; // ��ġ ������ ���̾�
    

    [SerializeField] private InputActionAsset playerInput;

    public Dictionary<int, InputData> activeActionDic { get; private set; }

    //������ Ŭ���� ����
    public InputData moveData { get; private set; }
    public InputData lookData { get; private set; }
    public InputData UI0Data{ get; private set; }
    public InputData UI1Data{ get; private set; }
    public InputData UI2Data{ get; private set; }
    public InputData UI3Data { get; private set; }
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
    }
    
    private void Update()
    {
        //if (Touchscreen.current == null) return;

        foreach (var touch in Touchscreen.current.touches)
        {
            int touchId = touch.touchId.ReadValue() - 1;
            Vector2 touchPos = touch.position.ReadValue();

            if(touch.press.isPressed && !activeActionDic.ContainsKey(touchId))
            {
                if (IsTouchOnUI(touch.position.ReadValue(),touchId) && touchId<4)
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
                        case 2:
                            BindAction(touchId, UI2Data, touchPos);
                            break;
                        case 3:
                            BindAction(touchId, UI3Data, touchPos);
                            break;
                    }
                       
                    
                    
                }
                else if (IsTouchOnJoystickArea(touchPos))
                {
                    touchState = etouchState.Player;

                    BindAction(touchId, moveData, touchPos);


                }
                else if (IsTouchableObjectAtPosition(touchPos))
                {
                    touchState = etouchState.Object;
                    BindAction(touchId, objectData, touchPos);
                }
                else
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchableLayer))
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
    private void OnUI2(InputAction.CallbackContext context)
    {
        UI2Data.SetCurrentPos(context.ReadValue<Vector2>());
    }
    private void OnUI3(InputAction.CallbackContext context)
    {
        UI3Data.SetCurrentPos(context.ReadValue<Vector2>());
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
            if(data.action == lookData.action)
            {
                data.action.AddBinding($"<Touchscreen>/touch{id}/delta");
            }
            else
            {
                data.action.AddBinding($"<Touchscreen>/touch{id}/position");
                data.SetStartTouchPos(id, touchPos);
            }
            
            data.action.Enable();            
        }
        
    }
    private void RomoveBindAction(int id)
    {
        if(activeActionDic.TryGetValue(id, out var data))
        {
            activeActionDic.Remove(id);
            //resetAction[id] = false;
            data.ResetData();
            data.action.Disable();
            data.action.RemoveAllBindingOverrides();

            


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
        UI2Data = new InputData(UIMap.FindAction("UI2"));
        UI3Data = new InputData(UIMap.FindAction("UI3"));

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
        UI2Data.action.performed += OnUI2;
        UI3Data.action.performed += OnUI3;
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
        UI2Data.action.Disable();
        UI3Data.action.Disable();
        objectData.action.Disable();
    }
}
