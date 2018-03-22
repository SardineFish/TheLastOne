using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class InputManager : Singleton<InputManager> {
    public float MaxMouseDistance = 500;

    [NonSerialized]
    public KeyCode[] KeyCodeList;
    public Camera camera;
    List<Action<KeyCode>> inputCallbackList = new List<Action<KeyCode>>();

    public Vector2 mousePosition { get { return Input.mousePosition; } }

    public Ray mouseToRay { get { return camera.ScreenPointToRay(Input.mousePosition); } }

    [ExecuteInEditMode]
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        KeyCodeList = typeof(KeyCode).GetFields()
            .Where(val => val.IsStatic && val.DeclaringType == typeof(KeyCode))
            .Select(field => (KeyCode)field.GetValue(null))
            .ToArray();
    }

    private void Update()
    {
        if (inputCallbackList.Count > 0)
        {
            var keyPressed = KeyCodeList.Where(key => Input.GetKeyDown(key)).FirstOrDefault();
            if(keyPressed != KeyCode.None)
            {
                while(inputCallbackList.Count>0)
                {
                    inputCallbackList[0](keyPressed);
                    inputCallbackList.RemoveAt(0);
                }
            }
        }
    }

    public virtual bool GetKeyDown(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }

    public virtual bool GetKeyUp(KeyCode keyCode)
    {
        return Input.GetKeyUp(keyCode);
    }

    public virtual bool GetKey(KeyCode keycode)
    {
        return Input.GetKey(keycode);
    }

    public virtual GameObject MouseOverObject()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,MaxMouseDistance))
        {
            return hit.transform.gameObject;
        }
        return null;
    }

    public virtual Vector3 MouseOnGround()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        var t = -ray.origin.y / ray.direction.y;
        var x = ray.direction.x * t + ray.origin.x;
        var z = ray.direction.z * t + ray.origin.z;
        return new Vector3(x, 0, z);
    }


    public virtual void WaitInputOnce(Action<KeyCode> callback)
    {
        inputCallbackList.Add(callback);
    }
}
