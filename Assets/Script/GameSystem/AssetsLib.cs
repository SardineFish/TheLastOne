using System;
using UnityEngine;
using System.Collections;

[Serializable]
public abstract class AssetsLib<TAssetLib, TAsset> : Singleton<TAssetLib>
    where TAssetLib : AssetsLib<TAssetLib, TAsset>
    where TAsset : UnityEngine.Object
{
    [Serializable]
    public class AssetObjectBase : AssetObject<TAssetLib, TAsset>
    {
    }
    [Serializable]
    public class AssetDictionaryBase : SerializableDictionary<string, TAsset> { }

    [SerializeField]
    public abstract SerializableDictionary<string, TAsset> AssetsLibrary { get; }

    public virtual TAsset GetAsset(string name) => AssetsLibrary[name];
}

[Serializable]
public class AssetObject<TAssetLib,TAsset> 
    where TAssetLib:AssetsLib<TAssetLib, TAsset>
    where TAsset : UnityEngine.Object
{
    [NonSerialized]
    TAssetLib assetLib;
    public TAssetLib AssetLib
    {
        get
        {
            if(assetLib == null)
                assetLib = typeof(TAssetLib).GetProperty("Instance").GetValue(null) as TAssetLib;
            if (assetLib == null)
                throw new Exception("Cannot get the instance of this asset lib.");
            return assetLib;
        }
    }

    [SerializeField]
    public string name;

    public TAsset Asset => AssetLib.GetAsset(name);
}
