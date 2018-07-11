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
            //serializedObject.ApplyModifiedProperties();
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
        }
    }
}