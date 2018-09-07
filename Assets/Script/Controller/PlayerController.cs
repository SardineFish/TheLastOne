using UnityEngine;
using UnityEditor;

public class PlayerController : EntityController
{
    public float k = 10;
    public override void Move(Vector2 direction)
    {
        var forward = CurrentFacing.ToVector3XZ();
        var right = Vector3.Cross( Vector3.up, forward).ToVector2XZ().normalized;
        var relativeDir = new Vector2(Vector2.Dot(direction,right),Vector2.Dot(direction,CurrentFacing.normalized));
        GetComponent<ActionManager>().Move(relativeDir);

    }
    public override void FaceTo(Vector3 direction)
    {
        var ang = MathUtility.MapAngle(MathUtility.ToAng(direction.ToVector2XZ()) - MathUtility.ToAng(CurrentFacing));

        if (Mathf.Abs(ang) > TurnSpeed * Time.deltaTime)
            ang = Mathf.Sign(ang) * TurnSpeed * Time.deltaTime;

        GetComponent<ActionManager>().Turn(ang);
    }
}