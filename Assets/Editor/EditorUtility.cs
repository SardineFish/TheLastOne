using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor
{
    public static class EditorUtility
    {
        public static AngularRange DrawAngularRangeField(string lable, AngularRange value)
        {
            var rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(22));
            rect.height = 20;
            EditorGUILayout.Space();
            var values = new float[] { value.low, value.high };
            EditorGUI.MultiFloatField(rect,new GUIContent(lable), new GUIContent[] { new GUIContent(" "), new GUIContent("~") }, values);
            EditorGUILayout.EndHorizontal();

            return new AngularRange(values[0], values[1]);
        }

        public static bool DrawFoldList(string lable, bool show, Func<bool> itemRenderingCallback)
        {
            show = EditorGUILayout.Foldout(show, lable);
            if (show)
            {
                GUIStyle style = new GUIStyle();
                style.margin.left = 40;
                EditorGUILayout.BeginVertical(style);
                while (itemRenderingCallback()) ;
                EditorGUILayout.EndVertical();
            }
            return show;
        }

        public static bool DrawFoldList(string lable, bool show,int count, Action<int> itemRenderingCallback)
        {
            show = EditorGUILayout.Foldout(show, lable);
            if (show)
            {
                GUIStyle style = new GUIStyle();
                style.margin.left = 40;
                EditorGUILayout.BeginVertical(style);
                for (var i = 0; i < count; i++)
                {
                    itemRenderingCallback(i);
                }
                EditorGUILayout.EndVertical();
            }
            return show;
        }

        public static Color HTMLColor(string color)
        {
            Color c;
            ColorUtility.TryParseHtmlString(color, out c);
            return c;
        }
        
        public static T ObjectField<T>(string label,T obj) where T:UnityEngine.Object
        {
            return EditorGUILayout.ObjectField(label, obj, typeof(T), true) as T;
        }
        public static T ObjectField<T>(T obj) where T : UnityEngine.Object
        {
            return EditorGUILayout.ObjectField(obj, typeof(T), true) as T;
        }

        public static void EditSerializableDictionary<TKey, TValue>(string lable, SerializableDictionary<TKey, TValue> dict)
        {
            EditSerializableDictionary(
                lable,
                dict,
                (key) => (TKey)(object)EditorGUILayout.TextField((string)(object)key),
                (value) => (TValue)(object)EditorGUILayout.ObjectField((UnityEngine.Object)(object)value, typeof(TValue), true)
                );
        }

        public static void EditSerializableDictionary<TKey, TValue>(string lable, SerializableDictionary<TKey, TValue> dict,Func<TKey,TKey> keyEditCallback,Func<TValue,TValue> valueEditCallback)
        {
            var style = new GUIStyle();
            style.margin.left = 60;
            //EditorGUILayout.BeginVertical(style);
            DrawFoldList(lable, true, dict.Count, (i) =>
            {
                EditorGUILayout.BeginHorizontal();
                dict.Keys[i] = keyEditCallback(dict.Keys[i]);
                dict.Values[i] = valueEditCallback(dict.Values[i]);
                EditorGUILayout.EndHorizontal();
            });
            if (GUILayout.Button("Add"))
            {
                dict.Add(default(TKey), default(TValue));
            }
        }
        /*
        public static void EditAssetObject<TAssetsLib,TAsset>(TAssetsLib assetsLib,TAssetsLib)
        {
            
        }*/
    }
}
