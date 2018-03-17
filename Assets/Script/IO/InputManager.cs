using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputManager : MonoBehaviour {
    public Vector2 Move;
    public Vector2 Turn;
    public Vector3 Target;
    public bool Action1;
    public bool Action2;
    public bool Action3;
    public bool Action4;

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
}
