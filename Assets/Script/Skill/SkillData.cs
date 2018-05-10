using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SkillData
{
    public SkillActionLib.SkillAction SkillAction;
    public SkillEffectSystem.SkillEffect SkillEffect;
    public SkillImpactSystem.SkillImpact SkillImpact;
}