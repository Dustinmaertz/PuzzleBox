using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

public class ChangeSkybox : MonoBehaviour
{
    private Material skybox;
    //private HandInter touchHandle;

    // Start is called before the first frame update
    void Start()
    {
        skybox = GetComponent<Renderer>().material;
        //touchHandle = gameObject.GetComponent<TouchHandler>();
        //touchHandle.OnTouchStarted.AddListener(() => ChangeMat());
    }
    
    public void ChangeMat()
    {
        RenderSettings.skybox = skybox;

    }
}
