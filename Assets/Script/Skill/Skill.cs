using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Skill : EntityBehavior<Entity>
{
    public float CoolDown = 1;

    protected float lastActiveTime = 0;
    // Use this for initialization
    void Start()
    {

    }

    public virtual bool Activate()
    {
        if (Time.time - lastActiveTime < CoolDown)
            return false;
        return true;
    }

    public virtual bool Activate(Vector3 target)
    {
        if (Time.time - lastActiveTime < CoolDown)
            return false;
        return true;
    }

    public virtual bool Activate(Entity target)
    {
        if (Time.time - lastActiveTime < CoolDown)
            return false;
        return true;
    }
}
