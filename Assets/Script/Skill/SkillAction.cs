using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum ActionFigure
{
    Sweep,
    UpSlash,
    DownAttack,
    Stab,
    TurnAround
}
[CreateAssetMenu(fileName = "SkillAction", menuName = "SkillAction")]
public class SkillAction : ScriptableObject,IWeightedObject
{
    [Serializable]
    public class SkillActionDict : SerializableDictionary<WeaponSystem.AssetObject, RuntimeAnimatorController> { }
    [HideInInspector]
    public SkillActionDict ActionsPerWeapon = new SkillActionDict();
    public Sprite Icon;
    public string DisplayName = "";
    public ActionFigure ActionFigure;
    [HideInInspector]
    public ImpactType AvailableImpactType;

    [SerializeField]
    private float weight = 1;
    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }
}