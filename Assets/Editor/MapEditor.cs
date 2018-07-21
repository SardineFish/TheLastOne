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
    [CustomEditor(typeof(Map))]
    public class MapEditor:UnityEditor.Editor
    {
        private void OnSceneGUI()
        {

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var map = target as Map;
            EditorUtility.EditWeightedList("Walls", true, map.Walls);
            EditorUtility.EditWeightedList("Walls with Door", true, map.WallsWithDoor);
            EditorUtility.EditWeightedList("Obstacles", true, map.Obstacles);
            UnityEditor.EditorUtility.SetDirty(map);

            EditorGUILayout.Space();
            if (GUILayout.Button("Generate"))
            {
                map.Generate();
            }
            if (GUILayout.Button("Clear"))
            {
                map.gameObject.ClearChildImmediate();
            }
        }


    }
}
