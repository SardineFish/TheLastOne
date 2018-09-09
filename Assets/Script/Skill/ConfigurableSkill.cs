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
    public GameObject SkillImpactPrefab;
    
    [NonSerialized]
    public GameObject SkillImpactInstance;
    public SkillEffectData[] SkillEffects;
    public ActivateMethod ActivateMethod;
    public float ImpactRadius = 2.85f;
    public float ImpactAngle = 360;
    public Vector3 ActivatePosition;

    [SerializeField]
    SkillData skillData;
    public SkillData SkillData
    {
        get { return skillData; }
        set { SetSkillData(value); }
    }

    private Vector3 activatePos;
    private Vector3 activateDirection;
    private Entity targetedEntity;

    public override bool Activate(params object[] additionalData)
    {
        var currentWeapon = WeaponSystem.Instance.GetAssetObject<WeaponSystem.AssetObject>(Entity.GetComponent<Equipments>().Selected);
        AnimatorController = SkillData.SkillAction.Asset.ActionsPerWeapon[currentWeapon];

        if(ActivateMethod == ActivateMethod.Position)
        {
            return Activate(InputManager.Instance.MouseOnGround());
        }
        else if (ActivateMethod == ActivateMethod.Direction)
        {
            return Activate(InputManager.Instance.MouseOnGround() - Entity.transform.position);
        }
        else if (ActivateMethod == ActivateMethod.Local)
        {
            return Activate(Entity.transform.forward);
        }
        else if (ActivateMethod == ActivateMethod.TargetedEntity)
        {
            var obj = InputManager.Instance.MouseOverObject();
            if (!obj)
                return false;
            var target = obj.GetComponent<Entity>();
            if (!target)
                return false;
            return Activate(target,additionalData);
        }
        return false;
    }

    public override bool Activate(Entity target, params object[] additionalData)
    {
        var currentWeapon = WeaponSystem.Instance.GetAssetObject<WeaponSystem.AssetObject>(Entity.GetComponent<Equipments>().Selected);
        AnimatorController = SkillData.SkillAction.Asset.ActionsPerWeapon[currentWeapon];

        switch (ActivateMethod)
        {
            case ActivateMethod.Direction:
            case ActivateMethod.Local:
            case ActivateMethod.Position:
                return Activate(additionalData);
        }

        if(!base.Activate(target,additionalData))
            return false;

        activatePos = Entity.transform.position;
        activateDirection = target.transform.position - Entity.transform.position;
        targetedEntity = target;

        AttachWeapon();
        return true;
    }

    public override bool Activate(Vector3 target, params object[] additionalData)
    {
        var currentWeapon = WeaponSystem.Instance.GetAssetObject<WeaponSystem.AssetObject>(Entity.GetComponent<Equipments>().Selected);
        AnimatorController = SkillData.SkillAction.Asset.ActionsPerWeapon[currentWeapon];

        if (ActivateMethod == ActivateMethod.TargetedEntity)
            return false;
        if (!base.Activate(target,additionalData))
            return false;

        else if (ActivateMethod == ActivateMethod.Local)
        {
            activatePos = Entity.transform.localToWorldMatrix.MultiplyPoint(ActivatePosition);
            activateDirection = target;
        }
        else if (ActivateMethod == ActivateMethod.Position)
        {
            activatePos = target;
            activateDirection = target - Entity.transform.position;
        }
        else if (ActivateMethod == ActivateMethod.Direction)
        {
            activatePos = Entity.transform.localToWorldMatrix.MultiplyPoint(ActivatePosition);
            activateDirection = target;
        }

        AttachWeapon();
        return true;
    }

    private void AttachWeapon()
    {
        return;
        var carrier = (Entity as LifeBody).PrimaryHand;

        if (Entity.GetComponent<SkillController>().ActiveSkill == this && carrier.Carrying)
            return;
        (Entity as LifeBody).PrimaryHand?.Release();
        if (Entity.GetComponent<Equipments>().Selected)
        {
            //Instantiate(WeaponPrefab).GetComponent<CarriableObject>().AttachTo((Entity as LifeBody).PrimaryHand);
            Instantiate(Entity.GetComponent<Equipments>().Selected).GetComponent<CarriableObject>().AttachTo((Entity as LifeBody).PrimaryHand);
            
        }
    }

    public override void OnSkillDamageStart()
    {
        SkillImpactInstance = Instantiate(SkillImpactPrefab);
        var impact = SkillImpactInstance.GetComponent<SkillImpact>();
        impact.Skill = this;
        impact.Creator = Entity;
        impact.SkillEffects = CalculateSkillEffect();
        impact.ImpactTarget = targetedEntity;
        impact.ImpactRadius = ImpactRadius;
        impact.ImpactAngle = ImpactAngle;
        switch (ActivateMethod)
        {
            case ActivateMethod.Direction:
                impact.Activate(Entity.transform.position, Entity.transform.forward);
                break;
            case ActivateMethod.Local:
                impact.Activate(Entity);
                break;
            case ActivateMethod.Position:
                impact.Activate(activatePos, activatePos);
                break;
            case ActivateMethod.TargetedEntity:
                impact.Activate(targetedEntity);
                break;
        }
        impact.StartDamage();
    }


    public override void OnSkillDamageEnd()
    {
        if (SkillImpactInstance)
        {
            SkillImpactInstance.GetComponent<SkillImpact>().EndDamage();
        }
    }

    public void SetSkillData(SkillData skillData)
    {
        this.skillData = skillData;
        ActivateMethod = ActivateMethod.Position;
        SkillImpactPrefab = skillData.SkillImpact.Asset as GameObject;
        SkillEffects = skillData.SkillEffect;
        switch (skillData.SkillImpact.Asset.GetComponent<SkillImpact>().ImpactType)
        {
            case ImpactType.Areal:
            case ImpactType.Collisional:
            case ImpactType.Penetrative:
                ActivateMethod = ActivateMethod.Direction;
                break;
            case ImpactType.Self:
                ActivateMethod = ActivateMethod.Local;
                break;
            case ImpactType.Targeted:
                ActivateMethod = ActivateMethod.TargetedEntity;
                break;
        }
    }

    public SkillEffectData[] CalculateSkillEffect()
    {
        var weapon = Entity.GetComponent<Equipments>().Selected.GetComponent<Weapon>();
        /*return  SkillEffects.Concat(weapon.SkillEffects.Concat(weapon.DefaultEffects))
            .GroupBy(effect => effect.SkillEffect)
            .Select(
                group => group.Merge(
                    new SkillEffectData(group.Key, 0),
                    (effect, effectData) =>
                    {
                        effectData.Multiple += effect.Multiple;
                        return effectData;
                    }))
            .ToArray();*/
        return SkillEffects.Concat(weapon.SkillEffects)
            .GroupBy(effect => effect.SkillEffect, effect => effect.Multiple)
            .Select(group => new SkillEffectData(group.Key, group.Sum()))
            .ToArray();
            
    }

}