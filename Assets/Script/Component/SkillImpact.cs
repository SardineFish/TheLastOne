using System.Collections;
using System.Collections.Generic;
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
    public Weapon Weapon;
    public Entity ImpactTarget = null;
    public Collider Collider;
    public float ImpactRadius = 1;
    public float ImpactRange = 360;
    public float ImpactHeight = 4;
    public float PenetrateDistance = 1;
    public float ImpactTime = 1;
    public bool SingleDamage = false;
    public bool DamageOnce = true;
    public Vector3 ImpactStartPosition;
    public Vector3 ImpactDirection;

    // Use this for initialization
    void Start()
    {
        transform.LookAt(ImpactDirection);

    }
    private void FixedUpdate()
    {
        if(ImpactType == ImpactType.Areal)
        {
            var colliders = Physics.OverlapCapsule(ImpactStartPosition, ImpactStartPosition + new Vector3(0, ImpactHeight, 0), ImpactRadius);

        }
    }

    public void Activate()
    {

    }
}
