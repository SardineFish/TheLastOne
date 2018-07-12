using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SkillData
{
    public SkillActionLib.AssetObject SkillAction;
    public SkillEffectData[] SkillEffect = new SkillEffectData[0];
    public SkillImpactSystem.AssetObject SkillImpact;
}