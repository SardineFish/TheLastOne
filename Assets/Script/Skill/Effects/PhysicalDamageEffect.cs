using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PhysicalDamageEffect : SkillEffect
{
    public float Damage = 0;

    public PhysicalDamageEffect(float damage)
    {
        Damage = damage;
    }

    public override void ApplyEffect(Entity effectFrom, Entity target)
    {
        if(target is LifeBody)
        {
            (target as LifeBody).HP -= Damage;
        }
    }
}