using UnityEngine;
using System.Collections;

public class SimpleSkill : AnimationSkill
{
    public override bool Activate(params object[] additionalData)
    {
        if (!base.Activate(additionalData))
            return false;
        if (!Entity.GetComponent<SimpleActionManager>().ChangeAction(AnimatorController))
            return false;

        //Entity.GetComponent<SimpleActionManager>().CurrentAnimatorPlayable.SetTrigger("active");
        return true;
    }
}
