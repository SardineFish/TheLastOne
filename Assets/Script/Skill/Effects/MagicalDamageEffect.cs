using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicalDamage",menuName ="SkillEffect/MagicalDamage")]
public class MagicalDamageEffect:SkillEffect
{
    public float Damage;

    public MagicalDamageEffect(float damage):this()
    {
        Damage = damage;
    }
    public MagicalDamageEffect():base()
    {
        DisplayName = "Magical Damage";
    }

    public override void ApplyEffect(SkillImpact impact, Entity target, float multiple)
    {
        if (target is LifeBody)
        {
            (target as LifeBody).HP -= Damage * multiple;
        }
    }
}