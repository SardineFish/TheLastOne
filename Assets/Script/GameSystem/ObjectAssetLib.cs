using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectAssetLib<TSingleton> : AssetsLib<TSingleton, UnityEngine.Object> where TSingleton:ObjectAssetLib<TSingleton>
{
    [Serializable]
    public class AssetDictionary : AssetDictionaryBase { }

    [SerializeField]
    [HideInInspector]
    private AssetDictionary assetsLib = new AssetDictionary();
    public override SerializableDictionary<string, UnityEngine.Object> AssetsLibrary => assetsLib;
}
