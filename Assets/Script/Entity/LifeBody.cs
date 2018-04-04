using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LifeBody : Entity
{
    public float HP;
    public float Defence;
    Rigidbody rigidbody;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnMessage(Message msg)
    {
        if (msg is DamageMessage)
        {
            var damageMsg = msg as DamageMessage;
            HP -= damageMsg.PhysicalDamage + damageMsg.MagicalDamage;
            Debug.Log("Damaged");
            var dir = Vector3.Scale(transform.position - damageMsg.Sender.transform.position,new Vector3(1,0,1));
            GetComponent<SkillController>().MovementSkill.KnockBack(dir.normalized*damageMsg.KnockBack);
        }
        else if(msg is SkillImpactMessage)
        {
            foreach (var effect in (msg as SkillImpactMessage).SkillEffects)
            {
                effect.ApplyEffect(msg.Sender, this);
            }
        }
        else
            base.OnMessage(msg);
    }
}