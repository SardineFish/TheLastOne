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
    public Vector3 RelativePosition;
    public void Start()
    {
        MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    [ExecuteInEditMode]
    public void Update()
    {
        if (!MainCamera || !BindTarget)
            return;
        transform.position = MainCamera.WorldToScreenPoint(BindTarget.transform.position + RelativePosition); 
        //GetComponent<Camera>().screen
        //Quaternion.LookRotation()
    }
}