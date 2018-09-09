using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SkillRangeEffect", menuName = "SkillEffect/SkillRangeEffect")]
public class SkillRangeEffect : PropertyEffect
{
    public override void ApplyEffect(SkillImpact impact, float multiple)
    {
        impact.ImpactRadius *= multiple;
    }
}
