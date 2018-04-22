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
            var editPosInput = GUILayout.Toggle(editPos, "Edit UI Position", "Button");
            if (editPos != editPosInput)
            {
                if(editPosInput)
                {
                    currentTool = Tools.current;
                    Tools.current = Tool.None;
                }
                else
                {
                    Tools.current = currentTool;
                }
                editPos = editPosInput;
            }
            SceneView.RepaintAll();
        }
        private void OnSceneGUI()
        {
            var ui = target as UIHover;
            if(editPos)
            {
                ui.UIObject.GetComponent<Billboard>().RelativePosition = Handles.PositionHandle(ui.UIObject.GetComponent<Billboard>().RelativePosition + ui.transform.position,Quaternion.identity) - ui.transform.position;
            }
        }

        
    }
}
