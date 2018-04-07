using UnityEngine;
using UnityEditor;

namespace Assets.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ConfigurableSkill))]
    public class ConfigurableSkillEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        private void OnSceneGUI()
        {
            var skill = target as ConfigurableSkill;

            var radius = 0.5f;
            var length = 2;

            skill.ActivatePosition = skill.Entity.transform.worldToLocalMatrix.MultiplyPoint(
                Handles.PositionHandle(
                    skill.Entity.transform.localToWorldMatrix .MultiplyPoint(skill.ActivatePosition),
                    Quaternion.LookRotation(
                        skill.Entity.transform.forward,
                        skill.Entity.transform.up)));

            Handles.color = EditorUtility.HTMLColor("#AB47BC55");
            Handles.DrawSolidDisc(skill.Entity.transform.localToWorldMatrix.MultiplyPoint(skill.ActivatePosition), skill.Entity.transform.forward, radius);Handles.DrawSolidRectangleWithOutline(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1) }, Color.black, Color.white);
            Handles.color = Color.red;
            Handles.DrawLine(
                skill.Entity.transform.localToWorldMatrix.MultiplyPoint(skill.ActivatePosition)
                , skill.Entity.transform.localToWorldMatrix.MultiplyPoint(skill.ActivatePosition) + skill.Entity.transform.forward * length);
        }
    }
}