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
    [CustomEditor(typeof(UITemplateComponent))]
    public class UITemplateComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var template = target as UITemplateComponent;
            EditorUtility.DrawFoldList("Bindings: ", true, template.Bindings.Count, (i) =>
            {
                var bind = template.Bindings[i];
                EditorGUILayout.TextField("BindSource", bind.PathSource);
                EditorGUILayout.LabelField("TargetPath");
                EditorGUILayout.BeginHorizontal();
                var components = template.GetComponents<Component>();
                var paths = bind.PathTemplate.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                var componentName = paths.Length > 0 ? paths[0] : "";
                var nameList = components.Select(component => component.GetType().Name).ToList();
                var idx = nameList.IndexOf(componentName);
                //idx = idx < 0 ? 0 : idx;
                if(idx<0)
                {
                    idx = 0;
                    componentName = nameList[0];
                    bind.PathTemplate = nameList[0] + ".";
                }

                idx = EditorGUILayout.Popup(idx, nameList.ToArray());
                bind.PathTemplate = nameList[idx] + "." + EditorGUILayout.TextField(bind.PathTemplate.Substring(componentName.Length + 1));
                EditorGUILayout.EndHorizontal();
            });
            if (GUILayout.Button("Add"))
            {
                template.Bindings.Add(new BindingOption());
            }
        }
    }
}
