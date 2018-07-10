using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : OwnedObject,IWeightedObject {
    WeaponData data;
    public SkillEffect[] SkillEffects;
    public WeaponData WeaponData
    {
        get { return data; }
        set
        {
            data = value;
            SkillEffects = data.SkillEffects.Select(assetObj => assetObj.Asset).ToArray();
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
