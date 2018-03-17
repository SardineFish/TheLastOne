using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlyType
{
    Direction,
    Destination,
}
public class Fly : EntityBehavior<Entity> {
    public FlyType FlyType;
    public float Speed;
    public Vector3 Target;

    private void FixedUpdate()
    {
        var dir = Target;
        if (FlyType == FlyType.Destination)
            dir = Target - transform.position;
        transform.Translate(dir.normalized * Speed * Time.fixedDeltaTime, Space.World);
    }
}
