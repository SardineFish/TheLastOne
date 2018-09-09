using UnityEngine;
using System.Collections;


public abstract class PropertyEffect : SkillEffect
{
    public override void ApplyEffect(SkillImpact impact, Entity target, float multiple)
    {
        return;
    }

    public abstract void ApplyEffect(SkillImpact impact, float multiple);
}
