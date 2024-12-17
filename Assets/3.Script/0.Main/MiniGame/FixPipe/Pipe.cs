using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipe : MonoBehaviour
{
    //필수로 지나가야하는 곳 
    [SerializeField] protected bool isimportant;
    public bool Isimportant { get { return isimportant; } }

    [SerializeField] protected Image pipeimage;
    [SerializeField] protected MeshRenderer render;

    [SerializeField] protected bool isImageready;
    public bool IsImageReady => isImageready;

    public void SetIsImageready()
    {
        isImageready = !isImageready;
    }

    private void Awake()
    {
        TryGetComponent(out render);
    }

    public virtual void PipeImageSet()
    {
        if (!pipeimage.gameObject.activeSelf)
        {
            pipeimage.gameObject.SetActive(true);
        }
    }

}
