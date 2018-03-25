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
    [CustomEditor(typeof(PlayerInputManager))]
    public class PlayerInputEditor:UnityEditor.Editor
    {
        bool showKeys = true;
        int keySetIdx = -1;
        public override void OnInspectorGUI()
        {
            var playerInput = target as PlayerInputManager;
            if(Event.current.type == EventType.KeyDown)
            {
                var x = 0;
            }
            if (keySetIdx >= 0)
            {
                if(Event.current.type == EventType.KeyDown)
                {
                    playerInput.SkillKeys[keySetIdx] = Event.current.keyCode;
                    keySetIdx = -1;
                    Event.current.Use();
                }
                else if (Event.current.type == EventType.MouseDown)
                {
                    playerInput.SkillKeys[keySetIdx] = (KeyCode)Enum.Parse(typeof(KeyCode), "Mouse" + Event.current.button.ToString());
                    keySetIdx = -1;
                    Event.current.Use();
                }
            }
            playerInput.PlayerInControl = EditorGUILayout.ObjectField("Player in Control", playerInput.PlayerInControl, typeof(Entity), true) as Entity;
            playerInput.MovementInput = EditorGUILayout.ObjectField("Movement Input", playerInput.MovementInput, typeof(MovementInput), true) as MovementInput;
            var count = EditorGUILayout.IntField("Keys", playerInput.SkillKeys.Count);
            if (count > playerInput.SkillKeys.Count)
                while (playerInput.SkillKeys.Count < count)
                    playerInput.SkillKeys.Add(KeyCode.None);
            else if (count < playerInput.SkillKeys.Count)
                playerInput.SkillKeys.RemoveRange(count, playerInput.SkillKeys.Count - count);
            showKeys = EditorUtility.DrawFoldList("Keys", showKeys, playerInput.SkillKeys.Count, (i) =>
            {
                /*if (Event.current.type == EventType.KeyDown)
                    playerInput.SkillKeys[i] = Event.current.keyCode;*/
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString() + ": ",GUILayout.Width(30));
                playerInput.SkillKeys[i] = (KeyCode)EditorGUILayout.EnumPopup(playerInput.SkillKeys[i]);
                if (GUILayout.Button("Set"))
                    keySetIdx = i;
                EditorGUILayout.EndHorizontal();
                //var keyName = EditorGUILayout.
                //playerInput.SkillKeys[i] = InputManager.Current.KeyCodeList.Where(key=>key.ToString().ToLower() == editorgui)
            });
        }
    }
}