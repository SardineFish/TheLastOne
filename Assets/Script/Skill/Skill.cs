using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Skill : EntityBehavior<Entity>
{
    public float CoolDown = 1;

    public float MaxDistance = 5;
    protected float lastActiveTime = 0;
    // Use this for initialization
    void Start()
    {

    }

    public virtual bool Activate(params object[] additionalData)
    {
        if (Time.time - lastActiveTime < CoolDown)
            return false;
        return true;
    }

    public virtual bool Activate(Vector3 target, params object[] additionalData)
    {
        if (Time.time - lastActiveTime < CoolDown)
            return false;
        return true;
    }

    public virtual bool Activate(Entity target, params object[] additionalData)
    {
        if (Time.time - lastActiveTime < CoolDown)
            return false;
        return true;
    }

    public virtual void OnSkillDamageStart()
    {

    }
    public virtual void OnSkillDamageEnd() 
    {

    }
}
