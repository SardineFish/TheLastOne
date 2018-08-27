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
    [CustomEditor(typeof(SkillImpactSystem))]
    public class SkillImpactSystemEditor : AssetLibEditor<SkillImpactSystem, GameObject>
    {
        public override void OnInspectorGUI()
        {
            var skillImpactLib = target as SkillImpactSystem;
            SkillImpact.ImpactTypes.ForEach((impact) =>
            {
                if (!skillImpactLib.AssetsLibrary.ContainsKey(impact.ToString()))
                {
                    skillImpactLib.AssetsLibrary.Add(impact.ToString(), null);
                }
            });
            base.OnInspectorGUI();
            UnityEditor.EditorUtility.SetDirty(target);
        }
    }
}
