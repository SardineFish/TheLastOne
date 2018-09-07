using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SimpleLifeController : EntityController
{
    public RuntimeAnimatorController MovementAnimator;
    public RuntimeAnimatorController AttackAnimator;
    public RuntimeAnimatorController DeathAnimator;

    public override void Move(Vector2 direction)
    {
        if (direction.magnitude <= 0.01)
        {
            Entity.GetComponent<SimpleActionManager>().Move(new Vector2(0, 0));
            return;
        }
        var forward = CurrentFacing.ToVector3XZ();
        var right = Vector3.Cross(Vector3.up, forward).ToVector2XZ().normalized;
        var relativeDir = new Vector2(Vector2.Dot(direction, right), Vector2.Dot(direction, CurrentFacing.normalized));

        TurnTo(direction);

        Entity.GetComponent<SimpleActionManager>().Move(new Vector2(0, 1));
    }

    public override void FaceTo(Vector3 direction)
    {
        return;
        var ang = MathUtility.MapAngle(MathUtility.ToAng(direction.ToVector2XZ()) - MathUtility.ToAng(CurrentFacing));

        if (Mathf.Abs(ang) > TurnSpeed * Time.deltaTime)
            ang = Mathf.Sign(ang) * TurnSpeed * Time.deltaTime;
    }

    public override void TurnTo(Vector2 direction)
    {
        var ang = MathUtility.MapAngle(MathUtility.ToAng(direction) - MathUtility.ToAng(CurrentFacing));

        if (Mathf.Abs(ang) > TurnSpeed * Time.deltaTime)
            ang = Mathf.Sign(ang) * TurnSpeed * Time.deltaTime;

        //Entity.transform.Rotate(0, -ang, 0, Space.Self);
        Entity.GetComponent<SimpleActionManager>().Turn(ang);
    }

}