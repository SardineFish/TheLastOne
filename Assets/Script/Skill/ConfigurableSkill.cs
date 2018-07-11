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
    public WeaponSystem.AssetObject WeaponData;
    
    [NonSerialized]
    public GameObject SkillImpactInstance;
    public SkillEffect[] SkillEffects;
    public ActivateMethod ActivateMethod;
    public float ImpactRadius = 1;
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

    public override void OnWeaponDamageStart()
    {
        SkillImpactInstance = Instantiate(SkillImpactPrefab);
        var impact = SkillImpactInstance.GetComponent<SkillImpact>();
        impact.Creator = Entity;
        impact.SkillEffects = SkillEffects;
        impact.Activate(activatePos, activateDirection);
        impact.ImpactTarget = targetedEntity;
        impact.ImpactRadius = ImpactRadius;
        impact.ImpactAngle = ImpactAngle;

    }

    public override void OnWeaponDamageEnd()
    {
        if (SkillImpactInstance)
            SkillImpactInstance.GetComponent<SkillImpact>().Distruct();
    }

    public void SetSkillData(SkillData skillData)
    {
        this.skillData = skillData;
        SkillImpactPrefab = skillData.SkillImpact.Asset as GameObject;
        SkillEffects = skillData.SkillEffect
            .Select(assetObj => assetObj.Asset as SkillEffect)
            .ToArray();
    }

}