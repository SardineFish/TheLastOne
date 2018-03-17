using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInputManager : InputManager
{
    public EntityController Controller;
    public override void SetMove(Vector2 direction)
    {
        base.SetMove(direction);

        if (!Controller)
            return;
        if (direction.magnitude > 0)
            Controller.Walk(direction);
        else
            Controller.Stop();
    }

    public override void Action1Pressed()
    {
        base.Action1Pressed();
        Controller.ActivateSkill(0, Target);
    }
    public override void Action2Pressed()
    {
        base.Action2Pressed();
        Controller.ActivateSkill(1, Target);
    }
}