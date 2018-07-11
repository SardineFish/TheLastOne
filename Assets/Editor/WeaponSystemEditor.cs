using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(WeaponSystem))]
    public class WeaponSystemEditor : AssetLibEditor<WeaponSystem, GameObject>
    {
        public override void OnInspectorGUI()
        {
            var weaponSystem = target as WeaponSystem;
            //weaponSystem.obj = null;
            weaponSystem.obj = new WeaponSystem.AssetObject() { name = weaponSystem.AssetsLibrary.Keys[0] };
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}
