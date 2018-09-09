using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ImpactType : int
{
    Collisional = 1,
    Areal = 2,
    Penetrative = 4,
    Self = 8,
    Targeted = 16,
}
public class SkillImpact : MonoBehaviour,IWeightedObject {
    public const float BaseArialRadius = 2.85f;
    public const float BaseCollisionRadius = 1;
    
    public static ImpactType[] ImpactTypes { get { return Enum.GetValues(typeof(ImpactType)).Cast<ImpactType>().ToArray(); } }
    public ImpactType ImpactType;
    public Entity Creator;
    public Entity ImpactTarget = null;
    public Collider Collider;
    public SkillEffectData[] SkillEffects;
    public Skill Skill;
    public float ImpactRadius = 1;
    public float ImpactAngle = 360;
    public float ImpactHeight = 4;
    public float PenetrateDistance = 1;
    public float LifeTime = 1;
    [SerializeField]
    public string DisplayName;
    public bool SingleDamage = false;
    public bool DamageOnce = true;
    public Vector3 ImpactStartPosition;
    public Vector3 ImpactDirection;

    public Sprite Icon;
    [SerializeField]
    private float weight = 1;
    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    bool damageStarted = false;
    float ImpactStartTime;
    List<Entity> hitEntities = new List<Entity>();
    // Use this for initialization
    void Start()
    {

    }
    
    private void Impact(Entity target)
    {
        if (target == Creator)
            return;
        // Send an impact message to the entity being impacted containing the effects of the skill
        new SkillImpactMessage(this, SkillEffects).Dispatch(target);
    }

    public void Distruct()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (LifeTime > 0 && Time.fixedTime - ImpactStartTime > LifeTime)
        {
            Distruct();
            return;
        }
        if (SingleDamage && hitEntities.Count > 0)
            return;

        if (!damageStarted)
            return;

        // Do damage
        if (ImpactType == ImpactType.Areal)
        {
            var colliders = Physics.OverlapCapsule(transform.position, transform.position + transform.up * ImpactHeight, ImpactRadius);
            foreach(var collider in colliders)
            {
                var entity = collider.GetComponent<Entity>();
                if (!entity)
                    continue;
                Debug.DrawLine(transform.position, collider.transform.position, Color.green);

                // Check hit point in impact range

                if (Vector2.Dot(entity.transform.position.ToVector2XZ() - transform.position.ToVector2XZ(), ImpactDirection.ToVector2XZ()) < Mathf.Cos(ImpactAngle * Mathf.Deg2Rad))
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
        else if (ImpactType == ImpactType.Collisional)
        {
            transform.position = new Vector3(transform.position.x, ImpactHeight, transform.position.z);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*if (!damageStarted)
            return;*/
        if(ImpactType == ImpactType.Collisional)
        {
            if (SingleDamage && hitEntities.Count > 0)
                return;

            var entity = other.GetComponent<LifeBody>();
            if (entity == Creator)
                return;
            if (!entity)
                return;

            // Make sure to damage it once
            if (DamageOnce && hitEntities.Contains(entity))
                return;
            hitEntities.Add(entity);

            Impact(entity);

            if (SingleDamage)
                Distruct();
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
        else if (ImpactType == ImpactType.Areal)
        {
            ImpactRadius = ImpactRadius * BaseArialRadius;
        }
        else if (ImpactType == ImpactType.Collisional)
        {
            ImpactRadius = ImpactRadius * BaseCollisionRadius;
            GetComponent<SphereCollider>().radius = ImpactRadius;
        }
        transform.position = ImpactStartPosition;
        transform.rotation *= Quaternion.FromToRotation(transform.forward, ImpactDirection);
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

    public void StartDamage()
    {
        damageStarted = true;
    }

    public void EndDamage()
    {
        damageStarted = false;
    }

    private void OnDrawGizmos()
    {
        
    }
}
