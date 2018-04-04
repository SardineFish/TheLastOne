using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ImpactType
{
    Targeted,
    Collisional,
    Areal,
    Penetrative,
}
public class SkillImpact : MonoBehaviour {
    public ImpactType ImpactType;
    public Entity Creator;
    public Entity ImpactTarget = null;
    public Collider Collider;
    public SkillEffect[] SkillEffects;
    public float ImpactRadius = 1;
    public float ImpactAngle = 360;
    public float ImpactHeight = 4;
    public float PenetrateDistance = 1;
    public float ImpactTime = 1;
    public bool SingleDamage = false;
    public bool DamageOnce = true;
    public Vector3 ImpactStartPosition;
    public Vector3 ImpactDirection;

    float ImpactStartTime;
    List<Entity> hitEntities = new List<Entity>();
    // Use this for initialization
    void Start()
    {
        transform.LookAt(ImpactDirection);

    }
    
    private void Impact(Entity target)
    {
        // Send an impact message to the entity being impacted containing the effects of the skill
        new SkillImpactMessage(Creator, SkillEffects).Dispatch(target);
    }

    public void Distruct()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if(ImpactTime>0 && Time.fixedTime -  ImpactStartTime>ImpactTime)
        {
            Distruct();
            return;
        }
        if (SingleDamage && hitEntities.Count > 0)
            return;

        if (ImpactType == ImpactType.Areal)
        {
            var hits = Physics.CapsuleCastAll(ImpactStartPosition, ImpactStartPosition + new Vector3(0, ImpactHeight, 0), ImpactRadius, transform.forward);
            foreach(var hit in hits)
            {
                var entity = hit.transform.GetComponent<Entity>();
                if (!entity)
                    continue;

                // Check hit point in impact range
                if (Vector3.Dot(hit.point - transform.position, ImpactDirection) < Mathf.Cos(ImpactAngle * Mathf.Deg2Rad))
                    continue;

                // Make sure to damage it once
                if (DamageOnce && hitEntities.Contains(entity))
                    continue;

                hitEntities.Add(entity);

                Impact(entity);
            }
        }
        else if (ImpactType == ImpactType.Penetrative)
        {
            var colliders = Physics.OverlapCapsule(transform.position, transform.position + ImpactDirection * PenetrateDistance, ImpactRadius);

            foreach (var collider in colliders)
            {
                var entity = collider.GetComponent<LifeBody>();
                if (!entity)
                    continue;

                // Make sure to damage it once
                if (DamageOnce && hitEntities.Contains(entity))
                    continue;
                hitEntities.Add(entity);

                Impact(entity);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(ImpactType == ImpactType.Collisional)
        {
            if (ImpactTime > 0 && Time.fixedTime - ImpactStartTime > ImpactTime)
            {
                Distruct();
                return;
            }
            if (SingleDamage && hitEntities.Count > 0)
                return;

            var entity = other.GetComponent<LifeBody>();

            if (!entity)
                return;

            // Make sure to damage it once
            if (DamageOnce && hitEntities.Contains(entity))
                return;
            hitEntities.Add(entity);

            Impact(entity);
        }
    }

    public void Activate()
    {
        if(ImpactType == ImpactType.Targeted)
        {
            Impact(ImpactTarget);
            Distruct();
            return;
        }
        transform.position = ImpactStartPosition;
        hitEntities = new List<Entity>();
        ImpactStartTime = Time.fixedTime;
    }

    public void Activate(Entity target)
    {
        ImpactTarget = target;
        Activate();
    }

    public void Activate(Vector3 position, Vector3 direction)
    {
        ImpactStartPosition = position;
        ImpactDirection = direction;
        Activate();
    }
}
