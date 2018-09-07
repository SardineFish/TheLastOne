using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EntityController : EntityBehavior<Entity>
{
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
    void Start()
    {
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
    void Update()
    {
        CurrentFacing = new Vector2(transform.forward.x, transform.forward.z);
    }

    public virtual void Move(Vector2 direction)
    {

    }
    public virtual void FaceTo(Vector3 direction)
    {
    }

    public virtual void TurnTo(Vector2 direction)
    {

    }
}
