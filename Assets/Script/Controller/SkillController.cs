using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class SkillController : EntityBehavior<Entity> {
    public Skill[] Skills;
    Skill activeSkill;
    public Skill ActiveSkill;
    public MovementSkill MovementSkill;
	// Use this for initialization
	void Start () {
        if (Application.isPlaying)
        {
            ActiveSkill = MovementSkill;
            MovementSkill.Activate();
        }
    }
	
    [ExecuteInEditMode]
	// Update is called once per frame
	void Update () {
        UpdateSkills();
    }

    public void OnAnimationEvent()
    {

    }

    public void OnWeaponDamageStart()
    {
        if(ActiveSkill)
            ActiveSkill.OnSkillDamageStart();
    }

    public void OnWeaponDamageEnd()
    {
        if(ActiveSkill)
            ActiveSkill.OnSkillDamageEnd();
    }

    public bool ActivateSkill(int idx)
    {
        if (Skills.Length > idx)
        {
            if (Skills[idx].Activate())
            {
                ActiveSkill = Skills[idx];
                Debug.Log("Activated " + ActiveSkill.name);
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
                Debug.Log("Activated " + ActiveSkill.name);
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
                Debug.Log("Activated " + ActiveSkill.name);
                return true;
            }
        }
        return false;
    }

    public bool ActivateMovementSkill(Vector2 movement)
    {
        if( MovementSkill.Activate(movement.ToVector3XZ()))
        {
            ActiveSkill = MovementSkill;
            Debug.Log("Activated " + ActiveSkill.name);
            return true;
        }
        return false;
    }

    public bool ActivateSkill<T>() where T : Skill
    {
        return GetComponentInChildren<T>() ? GetComponentInChildren<T>().Activate() : false;
    }
    public bool ActivateSkill<T>(Vector2 direction) where T: Skill
    {
        return GetComponentInChildren<T>() ? GetComponentInChildren<T>().Activate(direction) : false;
    }

    public T CreateSkill<T>(string name = "Skill") where T: Skill
    {
        var skillObj = new GameObject(name);
        var skill = skillObj.AddComponent<T>();
        SceneManager.MoveGameObjectToScene(skillObj, Entity.transform.Find("Skills").gameObject.scene);
        AddSkill(skill);
        return (T)skill;
    }

    public void AddSkill(Skill skill, int idx = -1)
    {
        skill.gameObject.transform.parent = Entity.transform.Find("Skills");
        if (idx >= 0)
            skill.gameObject.transform.parent.SetSiblingIndex(idx);
        UpdateSkills();
    }

    public void RemoveSkill(Skill skill)
    {
        Destroy(skill.gameObject);
        UpdateSkills();
    }

    public void RemoveSkill(int idx)
    {
        RemoveSkill(Skills[idx]);
    }

    public void SetSkill(int idx, Skill skill)
    {
        if (skill.gameObject != Skills[idx].gameObject)
        {
            Destroy(Skills[idx].gameObject);
            AddSkill(skill, idx);
            UpdateSkills();
        }
    }

    public void ClearSkills()
    {
        Skills.ForEach(skill => Destroy(skill.gameObject));
        UpdateSkills();
    }

    public void UpdateSkills()
    {
        Skills = transform.Find("Skills").GetComponentsInChildren<Skill>().ToArray();
    }

}
