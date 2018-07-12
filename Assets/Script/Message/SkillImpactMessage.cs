using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SkillImpactMessage : Message
{
    public SkillEffectData[] SkillEffects;
    public SkillImpact Impact;
    public SkillImpactMessage(SkillImpact impact, params SkillEffectData[] skillEffects) : base(impact.Creator)
    {
        Impact = impact;
        SkillEffects = skillEffects;
    }
}