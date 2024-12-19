using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipe : MonoBehaviour
{
    [SerializeField] protected FixPipeGameManager pipegameManager;
    //필수로 지나가야하는 곳 
    [SerializeField] protected bool isimportant;
    [SerializeField] protected Image pipeimage;
    [SerializeField] protected PipeImportantPoint ippoint;
    public Image PipeImage { get { return pipeimage; } }
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

        if(isimportant && !ippoint.Equals(null))
        {
            ippoint.SetIPImage(pipeimage.gameObject.activeSelf);
        }
    }

}