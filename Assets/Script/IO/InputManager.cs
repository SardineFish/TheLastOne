using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InputManager : MonoBehaviour {
    public Vector2 Move;
    public Vector2 Turn;
    public Vector3 Target;
    public bool Action1;
    public bool Action2;
    public bool Action3;
    public bool Action4;

    KeyCode[] keyCodeList;

    private void Start()
    {
        keyCodeList = typeof(KeyCode).GetFields()
            .Where(val => val.IsStatic && val.DeclaringType == typeof(KeyCode))
            .Select(field => (KeyCode)field.GetValue(null))
            .ToArray();
    }

    private void Update()
    {
        var keyPressed = keyCodeList.Where((key) =>
        {
            if (Input.GetKeyDown(key))
                Debug.Log(key);
            return true;
        }).ToArray();
        Debug.Log(Input.GetAxis("Joystick X Axis"));
        
        //Input.GetJoystickNames
    }

    public virtual void SetTarget(Vector3 target)
    {
        Target = target;
    }

    public virtual void SetMove(Vector2 direction)
    {
        Move = direction;
    }

    public virtual void SetTurn(Vector2 direction)
    {
        Turn = direction;
    }

    public virtual void Action1Pressed()
    {
        Action1 = true;
    }
    public virtual void Action1Released()
    {
        Action2 = false;
    }
    public virtual void Action2Pressed() { Action2 = true; }
    public virtual void Action2Released() { Action2 = false; }
    public virtual void Action3Pressed() { Action3 = true; }
    public virtual void Action3Released() { Action3 = false; }
    public virtual void Action4Pressed() { Action4 = true; }
    public virtual void Action4Released() { Action4 = false; }

    public virtual void WaitInputOnce(Action<KeyCode> callback)
    {

    }
}
