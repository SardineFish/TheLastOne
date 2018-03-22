using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementInput : EntityBehavior<Entity>
{
    public KeyCode[] InputKeys = new KeyCode[0];
    public abstract Vector2 GetMovement();

    public abstract void Interrupt();
}
