using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PhysicalDamage", menuName = "SkillEffect/PhysicalDamage")]
public class PhysicalDamageEffect : SkillEffect
{
    
    public float Damage = 0;

    public PhysicalDamageEffect(float damage) : this()
    {
        Damage = damage;
    }
    public PhysicalDamageEffect():base()
    {
        DisplayName = "Physical Damage";
    }

    public override void ApplyEffect(SkillImpact impact, Entity target)
    {
        if(target is LifeBody)
        {
            (target as LifeBody).HP -= Damage;
            UnityEngine.Debug.Log(target.name + " Damaged");
        }
    }
}