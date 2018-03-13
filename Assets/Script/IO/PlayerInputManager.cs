using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInputManager:InputManager
{
    public EntityController Controller;
    public override void Move(Vector2 direction)
    {
        if (!Controller)
            return;
        if (direction.magnitude > 0)
            Controller.Walk(direction);
        else
            Controller.Stop();
    }
}