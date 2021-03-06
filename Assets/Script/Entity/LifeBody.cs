﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LifeBody : Entity
{
    public float HP;
    public float Defence;
    public float MaxHP = 100;
    public float MaxDefence = 100;
    public Carrier PrimaryHand;
    public Carrier SecondaryHand;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
    }
    public void OnMessage(DamageMessage damageMsg)
    {
        HP -= damageMsg.PhysicalDamage + damageMsg.MagicalDamage;
        Debug.Log("Damaged");
        var dir = Vector3.Scale(transform.position - damageMsg.Sender.transform.position, new Vector3(1, 0, 1));
        GetComponent<SkillController>().MovementSkill.KnockBack(dir.normalized * damageMsg.KnockBack);
    }
    public void OnMessage(SkillImpactMessage msg)
    {
        foreach (var effect in msg.SkillEffects)
        {
            effect.SkillEffect.Asset.ApplyEffect(msg.Impact, this, effect.Multiple);
        }
    }
    /*public void OnMessage(Message msg)
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
                effect.ApplyEffect((msg as SkillImpactMessage).Impact, this);
            }
        }
        else
            base.OnMessage(msg);
    }*/
}