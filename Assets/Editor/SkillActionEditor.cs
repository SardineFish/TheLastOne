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
            var skillAction = target as SkillAction;

            EditorUtility.EditSerializableDictionary("Actions", skillAction.ActionsPerWeapon,
                (key) => WeaponSystemEditor.AssetObjectField(key),
                (value) => EditorUtility.ObjectField(value)
                );
        }
    }
}