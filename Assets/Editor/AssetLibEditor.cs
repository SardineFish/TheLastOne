using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Assets.Editor
{
    public class AssetLibEditor<TAssetLib, TAsset> : UnityEditor.Editor
        where TAssetLib : AssetsLib<TAssetLib, TAsset>
        where TAsset : UnityEngine.Object
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var assetLib = target as AssetsLib<TAssetLib, TAsset>;
            EditorUtility.EditSerializableDictionary("Assets", assetLib.AssetsLibrary);
            /*
            EditorUtility.DrawFoldList("Assets:", true, assetLib.AssetsLibrary.Count, (i) =>
             {
                 
             });*/
        }

        public static TAssetObject AssetObjectField<TAssetObject>(string label,TAssetObject obj) where TAssetObject: AssetObject<TAssetLib, TAsset>, new()
        {
            var t = typeof(TAssetLib);
            TAssetLib assetLib = typeof(TAssetLib).GetField("Instance", BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public).GetValue(null) as TAssetLib;
            TAsset asset = obj?.Asset;
            var objGet = EditorGUILayout.ObjectField(label, asset, typeof(TAsset), true) as TAsset;
            return new TAssetObject() { name = assetLib.AssetsLibrary.KeyOf(objGet) };
        }

        public static TAssetObject AssetObjectField<TAssetObject>(TAssetObject obj) where TAssetObject : AssetObject<TAssetLib, TAsset>, new()
        {
            var t = typeof(TAssetLib);
            TAssetLib assetLib = typeof(TAssetLib).GetField("Instance", BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public).GetValue(null) as TAssetLib;
            TAsset asset = obj?.Asset;
            var objGet = EditorGUILayout.ObjectField(asset, typeof(TAsset), true) as TAsset;
            return new TAssetObject() { name = assetLib.AssetsLibrary.KeyOf(objGet) };
        }
    }
}
