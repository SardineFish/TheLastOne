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
    [HideInInspector]
    public List<SkillEffectData> SkillEffects = new List<SkillEffectData>();

    [SerializeField]
    private float weight = 1;
    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public RuntimeAnimatorController GetAction(GameObject weapon)
    {
        return ActionsPerWeapon[WeaponSystem.Instance.GetAssetObject<WeaponSystem.AssetObject>(weapon)];
    }
}