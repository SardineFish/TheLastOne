using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DamageMessage : Message
{
    public DamageMessage(Entity sender, float physicalDamage, float magicalDamage) : base(sender)
    {
        PhysicalDamage = physicalDamage;
        MagicalDamage = magicalDamage;
    }

    public float PhysicalDamage { get; private set; }
    public float MagicalDamage { get; private set; }

}