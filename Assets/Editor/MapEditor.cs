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
        MapEditor()
        {
            SceneView.onSceneGUIDelegate += OnSceneDraw;
        }
        ~MapEditor()
        {
            SceneView.onSceneGUIDelegate -= OnSceneDraw;
        }
        private void OnEnable()
        {
            //SceneView.onSceneGUIDelegate += OnSceneDraw;
        }
        private void OnDisable()
        {
            //SceneView.onSceneGUIDelegate -= OnSceneDraw;
        }
        public bool wallsVisible = true;
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
                map.Generated = false;
            }

            EditorGUILayout.Space();
            if (wallsVisible && GUILayout.Button("HideWalls"))
            {
                map.GetComponentsInChildren<Wall>().ForEach(wall => wall.GetComponent<Renderer>().enabled = false);
                wallsVisible = false;
            }
            else if(!wallsVisible && GUILayout.Button("ShowWalls"))
            {
                map.GetComponentsInChildren<Wall>().ForEach(wall => wall.GetComponent<Renderer>().enabled = true);
                wallsVisible = true;
            }
        }

        private void OnSceneDraw(SceneView sceneView)
        {
            var map = target as Map;
            if (map.Generated)
            {
                Handles.color = Color.cyan;
                var height = map.GetComponentsInChildren<Wall>().Max(wall => wall.Height);
                Handles.DrawWireCube(new Vector3(0, height / 2, 0), new Vector3(map.Width, height, map.Height));
                var color = Color.magenta;
                ColorUtility.TryParseHtmlString("#4DB6AC", out color);
                color.a = 0.5f;
                Handles.color = color;
                map.ForEach((node) =>
                {
                    var verts = new Vector3[]
                    {
                        node.Center + new Vector3(map.NodeSize / 2, 0, map.NodeSize / 2),
                        node.Center + new Vector3(map.NodeSize / 2, 0, -map.NodeSize / 2),
                        node.Center + new Vector3(-map.NodeSize / 2, 0, -map.NodeSize / 2),
                        node.Center + new Vector3(-map.NodeSize / 2, 0, map.NodeSize / 2),
                        node.Center + new Vector3(map.NodeSize / 2, 0, map.NodeSize / 2),
                    };
                    //Handles.DrawAAConvexPolygon(verts);
                    Handles.DrawPolyLine(verts);
                });
            }
        }
    }
}
