using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class SkillController : EntityBehavior<Entity> {
    public Skill[] Skills;
    public Skill ActiveSkill;
    public MovementSkill MovementSkill;
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

    public bool ActivateSkill(int idx)
    {
        if (Skills.Length > idx)
        {
            if (Skills[idx].Activate())
            {
                ActiveSkill = Skills[idx];
                return true;
            }
        }
        return false;
    }

    public bool ActivateSkill(int idx,Vector3 target)
    {
        if (Skills.Length > idx)
        {
            if (Skills[idx].Activate(target))
            {
                ActiveSkill = Skills[idx];
                return true;
            }
        }
        return false;
    }

    public bool ActivateSkill(int idx, Entity target)
    {
        if (Skills.Length > idx)
        {
            if (Skills[idx].Activate(target))
            {
                ActiveSkill = Skills[idx];
                return true;
            }
        }
        return false;
    }

    public bool ActivateMovementSkill(Vector2 movement)
    {
        return MovementSkill.Activate(movement.ToVector3XZ());
    }

}
