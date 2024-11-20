using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3D : MonoBehaviour, ITouchable
{
    [SerializeField] private int id;
    [SerializeField] private UI_LerpImage lerpimage;


    public void SetIDItem3D(int id)
    {
        this.id = id;
    }

    public void OnTouchEnd(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, TouchManager.Instance.getTouchDistance, TouchManager.Instance.getTouchableLayer))
        {
            if (hit.collider.gameObject.Equals(gameObject) && gameObject.CompareTag("Item3D"))
            {
                //lerpimage.gameObject.SetActive(true);
                //lerpimage.InputMovementInventory(this, position); 
                // �̰� ���߿� 

                //������ ��� 
                Debug.Log($"�̰� ���̵� : {id}");
                UI_InvenManager.Instance.GetItemByID(id);
                gameObject.SetActive(false);
            }
        }
    }

    public void OnTouchHold(Vector2 position)
    {

    }

    public void OnTouchStarted(Vector2 position)
    {

    }
}