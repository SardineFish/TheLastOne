using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EntityController : EntityBehavior<Entity> {
    public bool MovementLock = false;
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
    private SkillController skillController;

    int PropSpeed, PropMoveX, PropMoveY;
	// Use this for initialization

    [ExecuteInEditMode]
	void Start () {
        Animator = GetComponent<Animator>();
        PropSpeed = Animator.StringToHash("speed");
        PropMoveX = Animator.StringToHash("moveX");
        PropMoveY = Animator.StringToHash("moveY");
	}

    private void OnEnable()
    {
        skillController = Entity.GetComponent<SkillController>();
    }

    // Update is called once per frame
    void Update () {
        CurrentFacing = new Vector2(transform.forward.x, transform.forward.z);
	}



    public void Walk(Vector2 direction)
    {
        if (MovementLock)
            return;
        Run(direction);
    }

    public void Run(Vector2 direction)
    {
        if (MovementLock)
            return;
        Animator.SetFloat(PropSpeed, RunSpeed);
        TurnAround(direction);
        Animator.SetFloat(PropMoveX, CurrentFacing.x);
        Animator.SetFloat(PropMoveY, CurrentFacing.y);
    }

    public void Stop()
    {
        if (MovementLock)
            return;
        Animator.SetFloat(PropSpeed, 0);
        Animator.SetFloat(PropMoveX, 0);
        Animator.SetFloat(PropMoveY, 0);
    }

    public void TurnAround(Vector2 direction)
    {
        if (MovementLock)
            return;
        var ang = MathUtility.MapAngle(MathUtility.ToAng(direction) - MathUtility.ToAng(CurrentFacing));
        //Debug.Log(MathUtility.ToAng(direction));
        if (Mathf.Abs(ang) > TurnSpeed * Time.deltaTime)
            ang = Mathf.Sign(ang) * TurnSpeed * Time.deltaTime;
        transform.Rotate(0, -ang, 0, Space.Self);
        
        CurrentFacing = new Vector2(transform.forward.x, transform.forward.z);
    }

    public void Jump()
    {
        if (MovementLock)
            return;
    }

    public void FlyUp()
    {
        if (MovementLock)
            return;
    }

    public void FlyDown()
    {
        if (MovementLock)
            return;
    }

    public void ActivateSkill(int skillId,Vector3 target)
    {
        skillController.ActivateSkill(skillId, target);
    }

    public void ActivateSkill(int skillId,Entity target)
    {

    }
}
