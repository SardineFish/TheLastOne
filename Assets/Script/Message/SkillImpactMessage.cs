using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SkillImpactMessage : Message
{
    public SkillEffect[] SkillEffects;
    public SkillImpactMessage(Entity sender, params SkillEffect[] skillEffects) : base(sender)
    {
        SkillEffects = skillEffects;
    }
}