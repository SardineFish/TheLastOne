using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SkillAction", menuName = "SkillAction")]
public class SkillAction : ScriptableObject
{
    [Serializable]
    public class SkillActionDict : SerializableDictionary<WeaponSystem.WeaponAsset, RuntimeAnimatorController> { }
    [HideInInspector]
    public SkillActionDict ActionsPerWeapon = new SkillActionDict();
    public Sprite Icon;
    public string DisplayName = "";
}