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
    [CustomEditor(typeof(SkillImpact))]
    public class SkillImpactEditor:UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var impact = target as SkillImpact;
            impact.ImpactType = (ImpactType)EditorGUILayout.EnumPopup("Impact Type: ", impact.ImpactType);
            impact.DisplayName = EditorGUILayout.TextField("Display Name: ", impact.DisplayName);
            EditorGUILayout.Space();
            if(impact.ImpactType == ImpactType.Areal)
            {
                impact.ImpactRadius = EditorGUILayout.FloatField("Impact Radius: ", impact.ImpactRadius);
                impact.ImpactAngle = EditorGUILayout.Slider("Impact Angle: ", impact.ImpactAngle, 0, 360);
                impact.ImpactHeight = EditorGUILayout.FloatField("Impact Height: ", impact.ImpactHeight);
            }
            else if (impact.ImpactType == ImpactType.Collisional)
            {
                impact.Collider = impact.GetComponent<Collider>();
                EditorGUILayout.ObjectField("Collider: ", impact.Collider, typeof(Collider), false);
                impact.ImpactHeight = EditorGUILayout.FloatField("Impact Height: ", impact.ImpactHeight);

            }
            else if (impact.ImpactType == ImpactType.Penetrative)
            {
                impact.PenetrateDistance = EditorGUILayout.FloatField("Penetate Distance: ", impact.PenetrateDistance);
                impact.ImpactRadius = EditorGUILayout.FloatField("Impact Radius: ", impact.ImpactRadius);

            }
            else if(impact.ImpactType == ImpactType.Targeted)
            {
                EditorGUILayout.ObjectField("Target: ", impact.ImpactTarget, typeof(Entity), false);
            }
            EditorGUILayout.Space();
            impact.DamageOnce = EditorGUILayout.Toggle("Damage Once: ", impact.DamageOnce);
            impact.SingleDamage = EditorGUILayout.Toggle("Single Damage: ", impact.SingleDamage);
            EditorGUILayout.ObjectField("Creator: ", impact.Creator, typeof(Entity), false);
            EditorGUILayout.Vector3Field("Start Position: ", impact.ImpactStartPosition);
            EditorGUILayout.Vector3Field("Direction: ", impact.ImpactDirection);
            SceneView.RepaintAll();
        }

        private void OnSceneGUI()
        {
            var impact = target as SkillImpact;
            if(impact.ImpactType == ImpactType.Areal)
            {
                Handles.color = EditorUtility.HTMLColor("#F0629255");
                Handles.DrawSolidArc(
                    impact.transform.position, 
                    impact.transform.up, 
                    Quaternion.AngleAxis(-impact.ImpactAngle/2,impact.transform.up) * impact.transform.forward, 
                    impact.ImpactAngle, 
                    impact.ImpactRadius);
            }
            else if (impact.ImpactType == ImpactType.Penetrative)
            {
                Handles.DrawSolidRectangleWithOutline(
                    new Vector3[]{
                        impact.transform.position - impact.transform.right*impact.ImpactRadius,
                        impact.transform.position - impact.transform.right*impact.ImpactRadius + impact.transform.forward*impact.PenetrateDistance,
                        impact.transform.position + impact.transform.right*impact.ImpactRadius + impact.transform.forward*impact.PenetrateDistance,
                        impact.transform.position + impact.transform.right*impact.ImpactRadius, },
                    EditorUtility.HTMLColor("#F0629255"),
                    EditorUtility.HTMLColor("#E91E63"));


            }
        }
    }
}
