using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(UIHover),typeof(ValueBar))]
public class LifeBody : Entity
{
    public float HP;
    public float Defence;
    public Carrier PrimaryHand;
    public Carrier SecondaryHand;
    ValueBar HPBar;
    Rigidbody rigidbody;

    private void Start()
    {
        
        HPBar = GetComponent<ValueBar>();
        //HPBar = ScriptableObject.CreateInstance<ValueBar>();
        GetComponent<UIHover>().AddUI(HPBar);
        HPBar.Range = new Range(0, HP);
        HPBar.Value = HP;
    }

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
                effect.ApplyEffect((msg as SkillImpactMessage).Impact, this);
            }
        }
        else
            base.OnMessage(msg);
    }

    public virtual void Update()
    {
        HPBar.Value = HP;
    }
}