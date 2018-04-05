using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSkill : AnimationSkill
{
    public const string AnimMoveX = "moveX";
    public const string AnimMoveY = "moveY";
    public const string AnimSpeed = "speed";
    public const string AnimKnockBack = "knock_back";
    public const string AnimJump = "jump";
    public const string AnimNameKnockBack = "KnockBack";

    public float MaxSpeed = 1;
    public float TurnSpeed = 360;

    ActionManager actionManager;

    private void OnEnable()
    {
        actionManager = Entity.GetComponent<ActionManager>();
    }

    public virtual void Jump()
    {
        if (Activate())
            Entity.GetComponent<ActionManager>().CurrentAnimatorController.SetTrigger(AnimJump);
    }
    public virtual void KnockBack(Vector3 direction)
    {
        Entity.GetComponent<CustomRigidBody>().AddForce(direction, ForceMode.Impulse);
        if (Activate())
        {
            if (!Entity.GetComponent<ActionManager>().CurrentAnimatorController.GetCurrentAnimatorStateInfo(0).IsName(AnimNameKnockBack))
                Entity.GetComponent<ActionManager>().CurrentAnimatorController.SetTrigger(AnimKnockBack);
            TurnImmediate(-direction);
        }
    }
    public virtual void TurnImmediate(Vector3 direction)
    {
        var ang = MathUtility.MapAngle(MathUtility.ToAng(direction.ToVector2XZ()) - MathUtility.ToAng(Entity.GetComponent<EntityController>().CurrentFacing));
        Entity.transform.Rotate(0, -ang, 0, Space.Self);

    }
    public override bool Activate()
    {
        return Activate(Vector3.zero);
    }
    public override bool Activate(Vector3 direction)
    {
        if (!Application.isPlaying)
            return false;
        if (!Entity.GetComponent<ActionManager>().ChangeAction(AnimatorController))
            return false;

        if (direction.magnitude <= 0.01f)
        {
            if (Entity.GetComponent<SkillController>().ActiveSkill == this)
            {
                actionManager.CurrentAnimatorController.SetFloat(AnimSpeed, 0);
                actionManager.CurrentAnimatorController.SetFloat(AnimMoveX, 0);
                actionManager.CurrentAnimatorController.SetFloat(AnimMoveY, 0);
                return true;
            }
        }
        else if (Entity.GetComponent<ActionManager>().ChangeAction(AnimatorController))
        {
            actionManager.CurrentAnimatorController.SetFloat(AnimSpeed, Mathf.Clamp01(direction.magnitude) * MaxSpeed);
            actionManager.CurrentAnimatorController.SetFloat(AnimMoveX, direction.x);
            actionManager.CurrentAnimatorController.SetFloat(AnimMoveY, direction.z);

            var ang = MathUtility.MapAngle(MathUtility.ToAng(direction.ToVector2XZ()) - MathUtility.ToAng(Entity.GetComponent<EntityController>().CurrentFacing));

            if (Mathf.Abs(ang) > TurnSpeed * Time.deltaTime)
                ang = Mathf.Sign(ang) * TurnSpeed * Time.deltaTime;

            Entity.transform.Rotate(0, -ang, 0, Space.Self);
            Entity.GetComponent<EntityController>().CurrentFacing = new Vector2(transform.forward.x, transform.forward.z);
            return true;
        }
        return false;
    }
}
