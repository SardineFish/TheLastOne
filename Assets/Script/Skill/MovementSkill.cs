using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSkill : AnimationSkill
{
    public const string AnimMoveX = "moveX";
    public const string AnimMoveY = "moveY";
    public const string AnimSpeed = "speed";

    public float MaxSpeed = 1;
    public float TurnSpeed = 360;
    Animator animator;
    private void OnEnable()
    {
        animator = Entity.GetComponent<Animator>();
    }
    public override bool Activate(Vector3 direction)
    {
        if(Entity.GetComponent<ActionManager>().ChangeAction(AnimatorController))
        {
            animator.SetFloat(AnimSpeed, Mathf.Clamp01(direction.magnitude) * MaxSpeed);
            animator.SetFloat(AnimMoveX, direction.x);
            animator.SetFloat(AnimMoveY, direction.z);
            if(direction.magnitude >0)
            {
                var ang = MathUtility.MapAngle(MathUtility.ToAng(direction.ToVector2XZ()) - MathUtility.ToAng(Entity.GetComponent<EntityController>().CurrentFacing));

                if (Mathf.Abs(ang) > TurnSpeed * Time.deltaTime)
                    ang = Mathf.Sign(ang) * TurnSpeed * Time.deltaTime;

                Entity.transform.Rotate(0, -ang, 0, Space.Self);
                Entity.GetComponent<EntityController>().CurrentFacing = new Vector2(transform.forward.x, transform.forward.z);
            }
            return true;
        }
        return false;
    }
}
