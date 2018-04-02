using UnityEngine;
using UnityEditor;

namespace Assets.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BindObject))]
    public class BindObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var bindObj = target as BindObject;
            bindObj.BindTo = EditorGUILayout.ObjectField("Bind To: ", bindObj.BindTo, typeof(GameObject), true) as GameObject;
            EditorGUILayout.Space();
            bindObj.BindPosition = EditorGUILayout.Toggle("Bind Position: ", bindObj.BindPosition);
            bindObj.BindRotation = EditorGUILayout.Toggle("Bind Rotation: ", bindObj.BindRotation);
            //bindObj.BindScale = EditorGUILayout.Toggle("Bind Scale: ", bindObj.BindScale);
            EditorGUILayout.Space();
            if (GUILayout.Button("Set Relative"))
            {
                if(bindObj.BindTo)
                {
                    bindObj.relativePosition = bindObj.BindTo.transform.worldToLocalMatrix * (bindObj.transform.position - bindObj.BindTo.transform.position);
                    bindObj.relativeRotation = bindObj.transform.rotation * Quaternion.Inverse(bindObj.BindTo.transform.rotation);
                    bindObj.targetOriginRotation = bindObj.BindTo.transform.rotation;
                }
            }
            /*if(GUILayout.Button("Add All Children"))
            {
                AddChildren(bindObj.transform);
            }*/
        }


        void AddChildren(Transform trans)
        {
            for (var i = 0; i < trans.childCount; i++)
            {
                trans.GetChild(i).gameObject.AddComponent<BindObject>();
                AddChildren(trans.GetChild(i));
            }
        }
    }
}