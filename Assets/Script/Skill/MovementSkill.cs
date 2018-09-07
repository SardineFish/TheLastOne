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
        
    private void OnEnable()
    {
    }

    public virtual void Jump()
    {
        if (Activate())
            Entity.GetComponent<PlayerActionManager>().CurrentAnimatorPlayable.SetTrigger(AnimJump);
    }
    public virtual void KnockBack(Vector3 direction)
    {
        Entity.GetComponent<CustomRigidBody>().AddForce(direction, ForceMode.Impulse);
        if (Activate())
        {
            if (!Entity.GetComponent<PlayerActionManager>().CurrentAnimatorPlayable.GetCurrentAnimatorStateInfo(0).IsName(AnimNameKnockBack))
                Entity.GetComponent<PlayerActionManager>().CurrentAnimatorPlayable.SetTrigger(AnimKnockBack);
            TurnImmediate(-direction);
        }
    }
    public virtual void TurnImmediate(Vector3 direction)
    {
        var ang = MathUtility.MapAngle(MathUtility.ToAng(direction.ToVector2XZ()) - MathUtility.ToAng(Entity.GetComponent<EntityController>().CurrentFacing));
        Entity.transform.Rotate(0, -ang, 0, Space.Self);

    }

    void RelaseCarry()
    {
        //(Entity as LifeBody).PrimaryHand?.Release();
        //(Entity as LifeBody).SecondaryHand?.Release();

    }

    public override bool Activate(params object[] additionalData)
    {
        return Activate(Vector3.zero,additionalData);
    }
    public override bool Activate(Vector3 direction, params object[] additionalData)
    {
        if (!Application.isPlaying)
            return false;
        if(direction.magnitude>0)
            if (!Entity.GetComponent<PlayerActionManager>().ChangeAction(AnimatorController))
                return false;
        if (direction.magnitude == 0 && Entity.GetComponent<SkillController>().ActiveSkill != this)
            return false;
        if (direction.magnitude <= 0.01f)
        {
            if (Entity.GetComponent<SkillController>().ActiveSkill == this)
            {
                Entity.GetComponent<PlayerActionManager>().CurrentAnimatorPlayable.SetFloat(AnimSpeed, 0);
                Entity.GetComponent<PlayerActionManager>().CurrentAnimatorPlayable.SetFloat(AnimMoveX, 0);
                Entity.GetComponent<PlayerActionManager>().CurrentAnimatorPlayable.SetFloat(AnimMoveY, 0);
                RelaseCarry();
                return true;
            }
        }
        else if (Entity.GetComponent<PlayerActionManager>().ChangeAction(AnimatorController))
        {
            Entity.GetComponent<PlayerActionManager>().CurrentAnimatorPlayable.SetFloat(AnimSpeed, Mathf.Clamp01(direction.magnitude) * MaxSpeed);
            Entity.GetComponent<PlayerActionManager>().CurrentAnimatorPlayable.SetFloat(AnimMoveX, direction.x);
            Entity.GetComponent<PlayerActionManager>().CurrentAnimatorPlayable.SetFloat(AnimMoveY, direction.z);

            var ang = MathUtility.MapAngle(MathUtility.ToAng(direction.ToVector2XZ()) - MathUtility.ToAng(Entity.GetComponent<EntityController>().CurrentFacing));

            if (Mathf.Abs(ang) > TurnSpeed * Time.deltaTime)
                ang = Mathf.Sign(ang) * TurnSpeed * Time.deltaTime;

            Entity.transform.Rotate(0, -ang, 0, Space.Self);
            //Entity.GetComponent<EntityController>().CurrentFacing = new Vector2(transform.forward.x, transform.forward.z);
            RelaseCarry();
            return true;
        }
        return false;
    }
}
