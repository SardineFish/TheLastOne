using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor
{
    [CustomEditor(typeof(EnemyAI))]
    [CanEditMultipleObjects]
    public class EnemyAIEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var enemy = target as EnemyAI;
            var color = Color.green;
            color.a = 0.1f;
            Handles.color = color;
            Handles.DrawSolidDisc(enemy.transform.position + Vector3.up * enemy.VisualHight, Vector3.up, enemy.VisualRange);
            color = Color.red;
            color.a = 0.1f;
            Handles.color = color;
            Handles.DrawSolidDisc(enemy.transform.position, Vector3.up, enemy.AttackRange);

        }
    }
}