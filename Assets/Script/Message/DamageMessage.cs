using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DamageMessage : Message
{
    public DamageMessage(Entity sender, float physicalDamage, float magicalDamage = 0, float knockBack = 0) : base(sender)
    {
        PhysicalDamage = physicalDamage;
        MagicalDamage = magicalDamage;
        KnockBack = knockBack;
    }

    public float PhysicalDamage { get; private set; }
    public float MagicalDamage { get; private set; }

    public float KnockBack { get; private set; }

}