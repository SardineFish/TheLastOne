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
        bool editCarrier = false;
        Tool currentTool;
        private void OnSceneGUI()
        {
            var obj = target as CarriableObject;

            if (editCarrier)
            {

                obj.CarryPostion = obj.transform.worldToLocalMatrix.MultiplyPoint(Handles.PositionHandle(obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion), obj.transform.rotation * obj.CarryRotation));
                obj.CarryRotation = obj.transform.worldToLocalMatrix.rotation * Handles.RotationHandle(obj.transform.rotation * obj.CarryRotation, obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion));

            }
            float radius = 0.2f;
            Handles.color = EditorUtility.HTMLColor("#FFC107");
            Handles.DrawWireDisc(obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion), obj.transform.rotation * (obj.CarryRotation* Vector3.forward), radius);
            Handles.color = EditorUtility.HTMLColor("#FFE08233");
            Handles.DrawSolidDisc(obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion), obj.transform.rotation * (obj.CarryRotation * Vector3.forward), radius);
        }

        public override void OnInspectorGUI()
        {
            var obj = target as CarriableObject;
            obj.CarryPostion = EditorGUILayout.Vector3Field("Carry Position", obj.CarryPostion);
            obj.CarryRotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Carry Rotation", obj.CarryRotation.eulerAngles));

            var editPosInput = GUILayout.Toggle(editCarrier, "Edit UI Position", "Button");
            if (editCarrier != editPosInput)
            {
                if (editPosInput)
                {
                    currentTool = Tools.current;
                    Tools.current = Tool.None;
                }
                else
                {
                    Tools.current = currentTool;
                }
                editCarrier = editPosInput;
                SceneView.RepaintAll();
            }
        }
    }
}
