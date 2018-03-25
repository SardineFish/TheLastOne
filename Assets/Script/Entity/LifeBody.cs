using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LifeBody : Entity
{
    public float HP;
    public float Defence;

    public override void OnMessage(Message msg)
    {
        if (msg is DamageMessage)
        {
            var damageMsg = msg as DamageMessage;
            HP -= damageMsg.PhysicalDamage + damageMsg.MagicalDamage;
            Debug.Log("Damaged");
        }
        else
            base.OnMessage(msg);
    }
}