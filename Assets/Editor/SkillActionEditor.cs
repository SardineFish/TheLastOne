using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SkillAction))]
    public class SkillActionEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            UnityEditor.EditorUtility.SetDirty(target);
            var skillAction = target as SkillAction;
            //skillAction.DisplayName = "F";
            EditorUtility.EditSerializableDictionary("Actions", skillAction.ActionsPerWeapon,
                (key) => WeaponSystemEditor.AssetObjectField(key),
                (value) => EditorUtility.ObjectField(value)
                );

            var impactTypes = Enum.GetValues(typeof(ImpactType)).Cast<ImpactType>().ToArray();

            EditorUtility.DrawFoldList("Available Impact Types", true, impactTypes.Length, (i) =>
            {
                var result = EditorGUILayout.ToggleLeft(impactTypes[i].ToString(), (skillAction.AvailableImpactType & impactTypes[i]) == impactTypes[i]);
                skillAction.AvailableImpactType |= impactTypes[i];
                if (!result)
                    skillAction.AvailableImpactType ^= impactTypes[i];
            });
            

            
            //serializedObject.ApplyModifiedProperties();
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
        }
    }
}