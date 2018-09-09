using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : OwnedObject,IWeightedObject {
    WeaponData data;
    [HideInInspector]
    public List<SkillEffectData> SkillEffects = new List<SkillEffectData>();
    public WeaponData WeaponData
    {
        get { return data; }
        set
        {
            data = value;
            //SkillEffects = value.SkillEffects.ToList();
        }
    }
    [SerializeField]
    private float weight = 1;
    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    protected List<Entity> damagedEntities = new List<Entity>();
    
    private void OnEnable()
    {
        damagedEntities.Clear();
        
    }

    private void OnTriggerStay(Collider other)
    {
        /*
        if (!enabled)
            return;
        var entity = other.GetComponent<Entity>();

        if (!entity)
            return;

        if (DamageOnce && damagedEntities.Contains(entity))
            return;
        damagedEntities.Add(entity);
        var damageMsg = new DamageMessage(Owner, PhysicalDamage, MagicalDamage, KnockBack);
        damageMsg.Dispatch(entity);*/
    }
}
