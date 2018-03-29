using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public enum SkillActivate
{
    Targeted,
    Directional,
    Areal,
}

public class ConfigurableSkill : AnimationSkill
{
    public GameObject WeaponPrefab;
    public GameObject CastObjectPrefab;
    public float PhysicsDamage = 0;
    public float MagicalDamage = 0;
    public float DamageRange = 0;
    public float AvailableRange = 10;
    public SkillActivate SkillActivate;
    public bool EnableCast = false;

    public override bool Activate()
    {
        if(SkillActivate  == SkillActivate.Areal)
        {
            return Activate(InputManager.Current.MouseOnGround());
        }
        else if (SkillActivate == SkillActivate.Directional)
        {
            return Activate(InputManager.Current.MouseOnGround() - Entity.transform.position);
        }
        else if (SkillActivate == SkillActivate.Targeted)
        {
            var obj = InputManager.Current.MouseOverObject();
            if (!obj)
                return false;
            var target = obj.GetComponent<Entity>();
            if (!target)
                return false;
            return Activate(target);
        }
        return false;
    }

    public override bool Activate(Entity target)
    {
        
        return base.Activate(target);
    }

    public override bool Activate(Vector3 target)
    {
        return base.Activate(target);
    }
}