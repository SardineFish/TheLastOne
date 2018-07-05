using System;
using UnityEngine;
using System.Collections;
using System.Reflection;

[Serializable]
public abstract class AssetsLib<TAssetLib, TAsset> : Singleton<TAssetLib>
    where TAssetLib : AssetsLib<TAssetLib, TAsset>
    where TAsset : UnityEngine.Object
{
    [Serializable]
    public class AssetObject : AssetObjectType<TAssetLib, TAsset>
    {
        public AssetObject():base() { }
        public AssetObject(string name) : base(name) { }
    }
    [Serializable]
    public class AssetDictionaryBase : SerializableDictionary<string, TAsset> { }

    [SerializeField]
    public abstract SerializableDictionary<string, TAsset> AssetsLibrary { get; }

    public virtual TAsset GetAsset(string name) => AssetsLibrary[name];
    public virtual string GetName(TAsset asset) => AssetsLibrary.KeyOf(asset);
    public virtual AssetObject GetAssetObject(TAsset asset) => new AssetObject(GetName(asset));
}

[Serializable]
public class AssetObjectType<TAssetLib,TAsset> 
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
                assetLib = typeof(TAssetLib).GetField("Instance", BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public).GetValue(null) as TAssetLib;
            if (assetLib == null)
                throw new Exception("Cannot get the instance of this asset lib.");
            return assetLib;
        }
    }

    [SerializeField]
    public string name;

    public TAsset Asset => AssetLib.GetAsset(name);

    public AssetObjectType()
    {
        this.name = null;
    }
    public AssetObjectType(string name)
    {
        this.name = name;
    }
}
