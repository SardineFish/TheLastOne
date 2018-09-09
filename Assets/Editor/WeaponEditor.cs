using UnityEngine;
using UnityEditor;

namespace Assets.Editor
{
    [CustomEditor(typeof(Weapon))]
    [CanEditMultipleObjects]
    public class WeaponEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var weapon = target as Weapon;

            EditorUtility.DrawFoldList("Skill Effect", true, weapon.SkillEffects.Count, (i) =>
            {
                EditorUtility.EditSkillEffectData(weapon.SkillEffects[i]);
            }, () =>
            {
                weapon.SkillEffects.Add(new SkillEffectData());
            });
            UnityEditor.EditorUtility.SetDirty(target);
        }
    }
}