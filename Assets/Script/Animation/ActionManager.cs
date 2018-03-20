using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : EntityBehavior<LifeBody>
{
    public const string AnimTagEnd = "End";
    public const string AnimTagBegin = "Begin";
    public const string AnimTagGap = "Gap";

    public RuntimeAnimatorController DefaultMovement;

    Animator animator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        animator = Entity.GetComponent<Animator>();
	}

    public bool ChangeAction(RuntimeAnimatorController controller)
    {
        if (!animator)
            return false;
        if (animator.runtimeAnimatorController == controller)
            return true;
        var state = animator.GetCurrentAnimatorStateInfo(0);
        if(/*state.IsTag(AnimTagBegin) || */state.IsTag(AnimTagEnd) || state.IsTag(AnimTagGap))
        {
            animator.runtimeAnimatorController = controller;
            return true;
        }
        return false;
    }

    public bool EnableMovement()
    {
        return ChangeAction(DefaultMovement);
    }
}
