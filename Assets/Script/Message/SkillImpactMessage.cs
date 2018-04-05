using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SkillImpactMessage : Message
{
    public SkillEffect[] SkillEffects;
    public SkillImpact Impact;
    public SkillImpactMessage(SkillImpact impact, params SkillEffect[] skillEffects) : base(impact.Creator)
    {
        Impact = impact;
        SkillEffects = skillEffects;
    }
}