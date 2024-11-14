using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPuzzleCanvas : MonoBehaviour,ITouchable
{
    [SerializeField] private GameObject missionStart;
    [SerializeField] private GameObject missionExit;

    [SerializeField] private GameObject btnExit;

    [Header("SaveManager ����")]
    [SerializeField] protected int floorIndex;
    [SerializeField] protected int objectIndex;


    private Collider mask;
    [HideInInspector]
    public bool isClear;

    

    private Outline outline;

    private void Start()
    {
        if (TryGetComponent(out outline))
            outline.enabled = false;
        TryGetComponent(out mask);
        isClear = false;
    }

    
    public void OffKeypad()
    {
        
        mask.enabled = true;
        btnExit.SetActive(false);

        if (isClear)
        {
            missionExit.SetActive(true);
            missionStart.SetActive(false);
            Invoke("ClearEvent", 3f);
        }
        else
        {
            missionStart.SetActive(false);
            TouchManager.Instance.EnableMoveHandler(true);
        }
           



    }

    public void OnTouchStarted(Vector2 position)
    { 
    }

    public void OnTouchHold(Vector2 position)
    { 
    }

    public void OnTouchEnd(Vector2 position)
    {
        if (isClear) return;
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, TouchManager.Instance.getTouchDistance, TouchManager.Instance.getTouchableLayer))
        {
            if (hit.collider.gameObject.Equals(gameObject))
            {
               
                    TouchManager.Instance.EnableMoveHandler(false);
                    missionStart.SetActive(true);

                    mask.enabled = false;

                    btnExit.SetActive(true);
                
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isClear) return;
        if (other.CompareTag("MainCamera"))
        {
            outline.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isClear) return;
        if (other.CompareTag("MainCamera"))
        {
            outline.enabled = false;
        }
    }

    private void ClearEvent()
    {
        missionExit.SetActive(false);
        TouchManager.Instance.EnableMoveHandler(true);
    }

}
