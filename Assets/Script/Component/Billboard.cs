using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject BindTarget;
    public void Start()
    {
        GameObject.FindWithTag("MainCamera");
    }
    [ExecuteInEditMode]
    public void Update()
    {
        if (!MainCamera || !BindTarget)
            return;
        transform.position = MainCamera.WorldToScreenPoint(BindTarget.transform.position); 
        //GetComponent<Camera>().screen
        //Quaternion.LookRotation()
    }
}