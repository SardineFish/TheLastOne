using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SkillAction", menuName = "SkillAction")]
public class SkillAction : ScriptableObject,IWeightedObject
{
    [Serializable]
    public class SkillActionDict : SerializableDictionary<WeaponSystem.AssetObject, RuntimeAnimatorController> { }
    [HideInInspector]
    public SkillActionDict ActionsPerWeapon = new SkillActionDict();
    public Sprite Icon;
    public string DisplayName = "";

    [SerializeField]
    private int weight = 1;
    public int Weight
    {
        get { return weight; }
        set { weight = value; }
    }
}