using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EntityController : MonoBehaviour {
    public float WalkSpeed;
    public float TurnSpeed;
    public float RunSpeed;
    public float JumpSpeed;
    public float FlySpeed;
    public Animator Animator;

    public Vector2 CurrentFacing;
    public Vector2 CurrentMovement;
    public float CurrentSpeed;
    Vector2 MoveDirection;
    Vector2 TurnDirection;

    int PropSpeed, PropMoveX, PropMoveY;
	// Use this for initialization

    [ExecuteInEditMode]
	void Start () {
        Animator = GetComponent<Animator>();
        PropSpeed = Animator.StringToHash("speed");
        PropMoveX = Animator.StringToHash("moveX");
        PropMoveY = Animator.StringToHash("moveY");
	}
	
	// Update is called once per frame
	void Update () {
        CurrentFacing = new Vector2(transform.forward.x, transform.forward.z);
	}



    public void Walk(Vector2 direction)
    {
        Run(direction);
    }

    public void Run(Vector2 direction)
    {
        Animator.SetFloat(PropSpeed, RunSpeed);
        TurnAround(direction);
        Animator.SetFloat(PropMoveX, CurrentFacing.x);
        Animator.SetFloat(PropMoveY, CurrentFacing.y);
    }

    public void Stop()
    {
        Animator.SetFloat(PropSpeed, 0);
        Animator.SetFloat(PropMoveX, 0);
        Animator.SetFloat(PropMoveY, 0);
    }

    public void TurnAround(Vector2 direction)
    {
        var ang = MathUtility.MapAngle(MathUtility.ToAng(direction) - MathUtility.ToAng(CurrentFacing));
        //Debug.Log(MathUtility.ToAng(direction));
        if (Mathf.Abs(ang) > TurnSpeed * Time.deltaTime)
            ang = Mathf.Sign(ang) * TurnSpeed * Time.deltaTime;
        transform.Rotate(0, -ang, 0, Space.Self);
        
        CurrentFacing = new Vector2(transform.forward.x, transform.forward.z);
    }

    public void Jump()
    {

    }

    public void FlyUp()
    {

    }

    public void FlyDown()
    {

    }
}
