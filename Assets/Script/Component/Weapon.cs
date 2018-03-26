using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : OwnedObject {
    public float PhysicalDamage;
    public float MagicalDamage;
    public bool DamageOnce = true;

    protected List<Entity> damagedEntities = new List<Entity>();
    
    private void OnEnable()
    {
        damagedEntities.Clear();
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!enabled)
            return;
        var entity = other.GetComponent<Entity>();

        if (!entity)
            return;

        if (DamageOnce && damagedEntities.Contains(entity))
            return;
        damagedEntities.Add(entity);
        var damageMsg = new DamageMessage(Owner, PhysicalDamage, MagicalDamage);
        damageMsg.Dispatch(entity);
    }
}
