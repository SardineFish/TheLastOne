using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class SkillController : EntityBehavior<Entity> {
    public Skill[] Skills;
    public Skill ActiveSkill;
	// Use this for initialization
	void Start () {
		
	}
	
    [ExecuteInEditMode]
	// Update is called once per frame
	void Update () {
        Skills = GetComponentsInChildren<Skill>().Where(skill => !(skill is MovementSkill)).ToArray();
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
