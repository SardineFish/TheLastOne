using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EntityController : EntityBehavior<Entity> {
    public float WalkSpeed;
    public float TurnSpeed;
    public float RunSpeed;
    public float JumpSpeed;
    public float FlySpeed;
    public Animator Animator;


    public Vector2 CurrentFacing;
    public Vector2 CurrentMovement;
    public float CurrentSpeed;
    public MovementSkill MovementSkill;
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
        Run(direction);
    }

    public void Run(Vector2 direction)
    {
        MovementSkill.Activate(direction.ToVector3XZ());
    }

    public void Stop()
    {
        MovementSkill.Activate(Vector3.zero);
    }

    public void TurnAround(Vector2 direction)
    {
        MovementSkill.Activate(direction.ToVector3XZ().normalized * .01f);
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

    public void ActivateSkill(int skillId,Vector3 target)
    {
        skillController.ActivateSkill(skillId, target);
    }

    public void ActivateSkill(int skillId,Entity target)
    {

    }
}
