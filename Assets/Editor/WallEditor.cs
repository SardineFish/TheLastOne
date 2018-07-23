using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor
{
    [CustomEditor(typeof(Wall))]
    [CanEditMultipleObjects]
    public class WallEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            //Handles.CubeHandleCap(0, new Vector3(0, 0, 0), Quaternion.identity, 1, EventType.Repaint);
            //Handles.DrawAAConvexPolygon(new Vector3[] { Vector3.zero, Vector3.forward, Vector3.left, Vector3.up });
            //Handles.DrawAAPolyLine(new Vector3[] { Vector3.zero, Vector3.forward, Vector3.left, Vector3.up });
            //Handles.DrawDottedLines(new Vector3[] { Vector3.zero, Vector3.forward, Vector3.left, Vector3.up }, 0f);

            var wall = target as Wall;
            Handles.color = Color.cyan;
            Handles.ArrowHandleCap(0, wall.transform.position, wall.transform.rotation*Quaternion.LookRotation(wall.Normal), HandleUtility.GetHandleSize(wall.transform.position )*0.5f, EventType.Repaint);
        }

        public override void OnInspectorGUI()
        {
            var wall = target as Wall;
            base.OnInspectorGUI();
        }
    }
}
