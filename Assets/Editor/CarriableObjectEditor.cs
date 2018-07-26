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
    [CustomEditor(typeof(CarriableObject))]
    public class CarriableObjectEditor : UnityEditor.Editor
    {

        private void OnSceneGUI()
        {
            var obj = target as CarriableObject;

            float radius = 0.2f;

            obj.CarryPostion = obj.transform.worldToLocalMatrix.MultiplyPoint(Handles.PositionHandle(obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion), obj.transform.rotation * obj.CarryRotation));
            obj.CarryRotation =Quaternion.Inverse(obj.transform.rotation) * Handles.RotationHandle(obj.transform.rotation * obj.CarryRotation, obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion));

            Handles.color = EditorUtility.HTMLColor("#FFC107");
            Handles.DrawWireDisc(obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion), obj.CarryRotation * obj.transform.forward, radius);
            Handles.color = EditorUtility.HTMLColor("#FFE08233");
            Handles.DrawSolidDisc(obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion), obj.CarryRotation * obj.transform.forward, radius);


        }

        public override void OnInspectorGUI()
        {
            var obj = target as CarriableObject;
            obj.CarryPostion = EditorGUILayout.Vector3Field("Carry Position", obj.CarryPostion);
            if (GUILayout.Button("SetPosition"))
            {
                
            }
            obj.CarryRotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Carry Rotation", obj.CarryRotation.eulerAngles));
        }
    }
}
