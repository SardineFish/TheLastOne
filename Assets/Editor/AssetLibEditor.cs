using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

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

        public TAsset AssetField(TAsset obj) => EditorGUILayout.ObjectField(obj, typeof(TAsset), true) as TAsset;
    }
}
