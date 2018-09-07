using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleSkill : AnimationSkill
{
    public SkillImpact SkillImpact;
    public List<SkillEffectData> SkillEffects = new List<SkillEffectData>();
    public override bool Activate(params object[] additionalData)
    {
        if (!base.Activate(additionalData))
            return false;
        if (!Entity.GetComponent<SimpleActionManager>().ChangeAction(AnimatorController))
            return false;

        //Entity.GetComponent<SimpleActionManager>().CurrentAnimatorPlayable.SetTrigger("active");
        return true;
    }

    public override void OnSkillDamageStart()
    {
        var impact = Utility.Instantiate(SkillImpact.gameObject, gameObject.scene).GetComponent<SkillImpact>();
        impact.SkillEffects = SkillEffects.ToArray();
        impact.Creator = Entity;
        impact.Activate(Entity.transform.position, Entity.transform.forward);
        impact.StartDamage();

    }
}
