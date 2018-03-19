﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : EntityBehavior<Entity> {
    public Skill[] Skills;
    public Skill ActiveSkill;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAnimationEvent()
    {

    }

    public void ActivateSkill(int idx,Vector3 target)
    {
        if (Skills.Length > idx)
        {
            Skills[idx].Activate(target);
            ActiveSkill = Skills[idx];
        }
    }


}
