using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOBJ : MonoBehaviour, ITouchable
{
    [Header("isClear�� True�϶� ����� ī�޶�")]
    [SerializeField] private GameObject clearCamera;
    [Header("isClear�� false�϶� ����� ī�޶�")]
    [SerializeField] private GameObject cinemachine;

    private Animator anim;
    private readonly int openAnim = Animator.StringToHash("Open");
    private Outline outline;

    private bool isTouching;

    [Header("���� �� �ٸ�������Ʈ�� ��ȣ�ۿ��� �ʿ��ϸ� False")]
    [SerializeField] private bool isClear;

    //SaveManger ����
    [SerializeField] private int floorIndex;
    [SerializeField] private int objectIndex;


    private void Start()
    {
        if (TryGetComponent(out outline))
            outline.enabled = false;

        if (!TryGetComponent(out anim))
        {
            transform.parent.TryGetComponent(out anim);
        }
        isTouching = false;
        if (!isClear)
        {
            isClear = SaveManager.Instance.PuzzleState(floorIndex, objectIndex);

        }
    }

    public void OnTouchStarted(Vector2 position)
    {
        //SaveManager isinteracted(���� ��� ����)
        if(!isClear)
        {
            isClear = SaveManager.Instance.PuzzleState(floorIndex, objectIndex);

        }

        isTouching = !isTouching;
        if (isClear)
        {
            clearCamera.SetActive(isTouching);
            anim.SetBool(openAnim, isTouching);
        }
        else
        {
            cinemachine.SetActive(isTouching);
        }



    }
    public void OnTouchHold(Vector2 position)
    {

    }
    public void OnTouchEnd(Vector2 position)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainPlayer"))
        {
            outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainPlayer"))
        {
            outline.enabled = false;
        }
    }
}
