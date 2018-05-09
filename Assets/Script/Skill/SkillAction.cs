using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SkillAction", menuName = "SkillAction")]
public class SkillAction : ScriptableObject
{
    public SerializableDictionary<WeaponSystem.WeaponAsset, RuntimeAnimatorController> ActionsPerWeapon = new SerializableDictionary<WeaponSystem.WeaponAsset, RuntimeAnimatorController>();
}
