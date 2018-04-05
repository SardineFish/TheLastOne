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
    [CustomEditor(typeof(Carrier))]
    public class CarrierEditor:UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var obj = target as Carrier;

            float radius = 0.2f;

            Handles.color = EditorUtility.HTMLColor("#009688");
            Handles.DrawWireDisc(obj.transform.position, obj.transform.forward, radius);
            Handles.color = EditorUtility.HTMLColor("#80CBC433");
            Handles.DrawSolidDisc(obj.transform.position, obj.transform.forward, radius);
        }
    }
}
