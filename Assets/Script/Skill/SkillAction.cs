using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SkillAction", menuName = "SkillAction")]
public class SkillAction : ScriptableObject
{
    public SerializableDictionary<string, RuntimeAnimatorController> ActionsPerWeapon = new SerializableDictionary<string, RuntimeAnimatorController>();
}
