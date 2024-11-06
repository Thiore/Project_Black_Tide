using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator ani;
    private ReadInputData input;
    private bool isOpend = false;

    //��ȣ�ۿ� object ����
    [SerializeField] private GameObject openObj;

    private void Start()
    {
        ani = GetComponent<Animator>();

        TryGetComponent(out input);

    }

    private void Update()
    {
        if (input.isTouch && !isOpend)
        {
            ani.SetTrigger("Open");
            isOpend = true;
        }
    }
}
