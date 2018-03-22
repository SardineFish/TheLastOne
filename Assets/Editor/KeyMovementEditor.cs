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
    [CustomEditor(typeof(KeyboardMovement))]
    public class KeyMovementEditor : UnityEditor.Editor
    {
        int keySetIdx = -1;
        public override void OnInspectorGUI()
        {
            var keyMovement = target as KeyboardMovement;

            if (keyMovement.InputKeys ==null)
                keyMovement.InputKeys = new KeyCode[4];
            if (keyMovement.InputKeys.Length != 4)
                Array.Resize(ref keyMovement.InputKeys, 4);

            if (keySetIdx >= 0)
            {
                if (Event.current.type == EventType.KeyDown)
                {
                    keyMovement.InputKeys[keySetIdx] = Event.current.keyCode;
                    keySetIdx = -1;
                    Event.current.Use();
                }
                else if (Event.current.type == EventType.MouseDown)
                {
                    keyMovement.InputKeys[keySetIdx] = (KeyCode)Enum.Parse(typeof(KeyCode), "Mouse" + Event.current.button.ToString());
                    keySetIdx = -1;
                    Event.current.Use();
                }
            }

            EditorUtility.DrawFoldList("Input Keys", true, 4, (i) =>
            {
                string tag = "";
                switch (i)
                {
                    case KeyboardMovement.IdxHorizontalNegative:
                        tag = "Horizontal + :";
                        break;
                    case KeyboardMovement.IdxHorizontalPositive:
                        tag = "Horizontal - :";
                        break;
                    case KeyboardMovement.IdxVerticalNegative:
                        tag = "Vertical - :";
                        break;
                    case KeyboardMovement.IdxVerticalPositive:
                        tag = "Vertical + :";
                        break;
                }
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.EnumPopup(tag, keyMovement.InputKeys[i]);
                if (GUILayout.Button("Set"))
                    keySetIdx = i;
                EditorGUILayout.EndHorizontal();
            });
        }
    }
}
