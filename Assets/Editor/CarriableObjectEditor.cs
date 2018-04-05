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

            obj.CarryPostion = obj.transform.worldToLocalMatrix.MultiplyPoint(Handles.PositionHandle(obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion), obj.transform.rotation));

            Handles.color = EditorUtility.HTMLColor("#FFC107");
            Handles.DrawWireDisc(obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion), obj.transform.forward, radius);
            Handles.color = EditorUtility.HTMLColor("#FFE08233");
            Handles.DrawSolidDisc(obj.transform.localToWorldMatrix.MultiplyPoint(obj.CarryPostion), obj.transform.forward, radius);


        }


    }
}
