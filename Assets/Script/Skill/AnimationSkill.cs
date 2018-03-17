using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[ExecuteInEditMode]
public class AnimationSkill:Skill
{
    public const string AnimActiveTrigger = "active";
    public RuntimeAnimatorController AnimatorController;
    Animator animator;

    [ExecuteInEditMode]
    private void OnEnable()
    {
        animator = Entity.GetComponent<Animator>();
    }

    public override bool Activate()
    {
        if (base.Activate() && animator)
        {
            lastActiveTime = Time.time;

            if (Entity.GetComponent<Animator>().runtimeAnimatorController.Equals(AnimatorController))
            {
                animator.SetTrigger(AnimActiveTrigger);
            }
            else
            {
                animator.runtimeAnimatorController = AnimatorController;
            }
        }
        return true;
    }

    public override bool Activate(Vector3 target)
    {
        return base.Activate(target);
    }

    public override bool Activate(Entity target)
    {
        return base.Activate(target);
    }
}