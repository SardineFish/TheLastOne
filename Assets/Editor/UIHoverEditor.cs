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
    [CustomEditor(typeof(UIHover))]
    public class UIHoverEditor:UnityEditor.Editor
    {
        bool editPos = false;
        Tool currentTool;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            editPos = GUILayout.Toggle(editPos, "Edit UI Position", "Button");
            SceneView.RepaintAll();
        }
        private void OnSceneGUI()
        {
            if(editPos)
            {
                currentTool = Tools.current;
                Tools.current = Tool.None;
                
            }
        }

        
    }
}
