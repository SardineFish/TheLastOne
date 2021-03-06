﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Animations;

//[ExecuteInEditMode]
public class AnimationSkill:Skill
{
    public const string AnimActiveTrigger = "active";
    public RuntimeAnimatorController AnimatorController;
    ActionManagerBase actionManager => Entity.GetComponent<ActionManagerBase>();

    [ExecuteInEditMode]
    private void OnEnable()
    {
        if (!Entity)
            return;
        //actionManager = Entity.GetComponent<ActionManager>();
    }

    public override bool Activate(params object[] additionalData)
    {
        if (base.Activate(additionalData) && actionManager)
        {
            if(Entity.GetComponent<ActionManagerBase>().ChangeAction(AnimatorController))
            {
                lastActiveTime = Time.time;
                Entity.GetComponent<ActionManagerBase>().CurrentAnimatorPlayable.SetTrigger(AnimActiveTrigger);
                //animator.SetTrigger(AnimActiveTrigger);
                return true;
            }
        }
        return false;
    }

    public override bool Activate(Vector3 target, params object[] additionalData)
    {
        if(base.Activate(target,additionalData) && actionManager)
        {
            if (Entity.GetComponent<ActionManagerBase>().ChangeAction(AnimatorController))
            {
                lastActiveTime = Time.time;
                Entity.GetComponent<ActionManagerBase>().CurrentAnimatorPlayable.SetTrigger(AnimActiveTrigger);
                //animator.SetTrigger(AnimActiveTrigger);
                return true;
            }
        }
        return false;
    }

    public override bool Activate(Entity target, params object[] additionalData)
    {
        if (base.Activate(target,additionalData) && actionManager)
        {
            if (Entity.GetComponent<ActionManagerBase>().ChangeAction(AnimatorController))
            {
                lastActiveTime = Time.time;
                Entity.GetComponent<ActionManagerBase>().CurrentAnimatorPlayable.SetTrigger(AnimActiveTrigger);
                //animator.SetTrigger(AnimActiveTrigger);
                return true;
            }
        }
        return false;
    }

    public virtual void Update()
    {
        if (!Entity)
            return;
        if (Entity.GetComponent<SkillController>().ActiveSkill != this)
            return;
        if(actionManager)
        { 
            var state = actionManager.CurrentAnimatorPlayable.GetCurrentAnimatorStateInfo(0);
            if (state.IsTag(ActionManagerBase.AnimTagEnd) && Entity.GetComponent<ActionManagerBase>())
            {
                //Entity.GetComponent<ActionManagerBase>().EnableMovement();
                return;
            }
        }
    }
}