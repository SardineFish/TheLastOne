using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public enum ActivateMethod
{
    /// <summary>
    /// Activate skill to a specific Entity
    /// </summary>
    TargetedEntity,
    /// <summary>
    /// Activate skill to a specific direction
    /// </summary>
    Direction,
    /// <summary>
    /// Activate Skill at current position
    /// </summary>
    Local,
    /// <summary>
    /// Activate Skill at a specific position
    /// </summary>
    Position,
}

public class ConfigurableSkill : AnimationSkill
{
    public GameObject WeaponPrefab;
    public GameObject SkillImpactPrefab;
    
    [NonSerialized]
    public GameObject SkillImpactInstance;
    public SkillEffect[] SkillEffects;
    public ActivateMethod ActivateMethod;
    public float ImpactRadius = 1;
    public float ImpactAngle = 360;

    private Vector3 activatePosition;
    private Vector3 activateDirection;
    private Entity targetedEntity;

    public override bool Activate()
    {
        if(ActivateMethod == ActivateMethod.Position)
        {
            return Activate(InputManager.Current.MouseOnGround());
        }
        else if (ActivateMethod == ActivateMethod.Direction)
        {
            return Activate(InputManager.Current.MouseOnGround() - Entity.transform.position);
        }
        else if (ActivateMethod == ActivateMethod.Local)
        {
            return Activate(Entity.transform.position);
        }
        else if (ActivateMethod == ActivateMethod.TargetedEntity)
        {
            var obj = InputManager.Current.MouseOverObject();
            if (!obj)
                return false;
            var target = obj.GetComponent<Entity>();
            if (!target)
                return false;
            return Activate(target);
        }
        return false;
    }

    public override bool Activate(Entity target)
    {
        switch(ActivateMethod)
        {
            case ActivateMethod.Direction:
            case ActivateMethod.Local:
            case ActivateMethod.Position:
                return Activate();
        }
        activatePosition = Entity.transform.position;
        activateDirection = target.transform.position - Entity.transform.position;
        targetedEntity = target;
        return base.Activate(target);
    }

    public override bool Activate(Vector3 target)
    {
        if (ActivateMethod == ActivateMethod.TargetedEntity)
            return false;

        activatePosition = Entity.transform.position;
        activateDirection = target;
        return base.Activate(target);
    }

    public override void OnWeaponDamageStart()
    {
        SkillImpactInstance = Instantiate(SkillImpactPrefab);
        var impact = SkillImpactInstance.GetComponent<SkillImpact>();
        impact.SkillEffects = SkillEffects;
        impact.Activate(activatePosition, activateDirection);
        impact.ImpactTarget = targetedEntity;
        impact.ImpactRadius = ImpactRadius;
        impact.ImpactAngle = ImpactAngle;
    }

    public override void OnWeaponDamageEnd()
    {
        if (SkillImpactInstance)
            SkillImpactInstance.GetComponent<SkillImpact>().Distruct();
    }
}