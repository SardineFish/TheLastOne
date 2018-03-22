using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MouseMovement))]
    public class MouseMovementEditor : UnityEditor.Editor
    {
        bool setKey = false;
        public override void OnInspectorGUI()
        {
            var mouseMovement = target as MouseMovement;
            
            if (mouseMovement.InputKeys == null)
                mouseMovement.InputKeys = new KeyCode[1];
            if (mouseMovement.InputKeys.Length != 1)
                Array.Resize(ref mouseMovement.InputKeys, 1);

            if (setKey)
            {
                if (Event.current.type == EventType.KeyDown)
                {
                    mouseMovement.InputKeys[0] = Event.current.keyCode;
                    setKey = false;
                    Event.current.Use();
                }
                else if (Event.current.type == EventType.MouseDown)
                {
                    mouseMovement.InputKeys[0] = (KeyCode)Enum.Parse(typeof(KeyCode), "Mouse" + Event.current.button.ToString());
                    setKey = false;
                    Event.current.Use();
                }
            }

            base.OnInspectorGUI();
            if(GUILayout.Button("Set Key"))
            {
                setKey = true;
            }
        }
    }
}
