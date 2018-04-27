using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Script.Package.Modle;

[CustomEditor(typeof(BaseItem))]
[CanEditMultipleObjects]
public class PackageManager : Editor {
    //private BaseItem Item;

    //private SerializedProperty propType;
    //private SerializedProperty ChineseName, ItemImage, Introduction, PropEffect;

    ////prop name
    //private string[] propNames;       
    //// save selected prop name
    //private int selectIndex;           

    //public void OnEnable()
    //{
    //    Item = target as BaseItem;

    //    propType = serializedObject.FindProperty("propType");
    //    ItemImage = serializedObject.FindProperty("ItemImage");
    //    ChineseName = serializedObject.FindProperty("ChineseName");
    //    Introduction = serializedObject.FindProperty("Introduction");
    //    PropEffect = serializedObject.FindProperty("Effect");

    //    //get every prop name
    //    propNames = propType.enumNames;    
    //    //get prop index
    //    selectIndex = propType.enumValueIndex; 
    //}

    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update();

    //   GUILayout.Space(5);
    //    EditorGUILayout.PropertyField(ItemImage, GUILayout.ExpandWidth(true));
    //    EditorGUILayout.PropertyField(ChineseName, GUILayout.ExpandWidth(true));
    //    EditorGUILayout.PropertyField(Introduction, GUILayout.ExpandWidth(true));

    //    GUILayout.Space(10);
        
    //    //get prop arrays
    //    //make them by popup
    //    //return the selected prop
    //    int index = selectIndex;
    //    selectIndex = EditorGUILayout.Popup("PropType", index, propNames, GUILayout.ExpandWidth(true));

    //    GUILayout.Space(10);

    //    if (index != selectIndex)
    //    {
    //        //change selected value to enum
    //        Item.propType = (PropTypes)selectIndex;
    //        //打印选中的枚举值
    //        Debug.Log("selectIndex      :" + selectIndex);
    //        Debug.Log("selectName       :" + propNames[selectIndex]);
    //        Debug.Log("selectPropType  :" + (EventType)selectIndex);
    //        Debug.Log("PropType        :" + Item.propType);
    //    }

    //    if (GUI.changed)
    //    {
    //        EditorUtility.SetDirty(target);
    //    }

    //    // 保存序列化数据，否则会出现设置数据丢失情况
    //    serializedObject.ApplyModifiedProperties();
    //}

}

