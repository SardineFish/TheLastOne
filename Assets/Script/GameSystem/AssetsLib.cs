using System;
using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

[Serializable]
public abstract class AssetsLib<TAssetLib, TAsset> : Singleton<TAssetLib>
    where TAssetLib : AssetsLib<TAssetLib, TAsset>
    where TAsset : UnityEngine.Object
{
    [Serializable]
    public class AssetObjectBase : AssetObjectType<TAssetLib, TAsset>
    {
        public AssetObjectBase() : base() { }
        public AssetObjectBase(string name) : base(name) { }
    }
    [Serializable]
    public class AssetDictionaryBase : SerializableDictionary<string, TAsset> { }

    [SerializeField]
    public abstract SerializableDictionary<string, TAsset> AssetsLibrary { get; }

    public virtual TAsset GetAsset(string name) => AssetsLibrary[name];
    public virtual string GetName(TAsset asset) => AssetsLibrary.KeyOf(asset);
    public virtual TAssetObject GetAssetObject<TAssetObject>(TAsset asset) where TAssetObject : AssetObjectBase, new() => new TAssetObject() { name = GetName(asset) } as TAssetObject;
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
        //this.name = null;
    }
    public AssetObjectType(string name)
    {
        this.name = name;
    }

    public static bool operator ==(AssetObjectType<TAssetLib, TAsset> a, AssetObjectType<TAssetLib, TAsset> b) => a!=null && b!=null && a.name == b.name;
    public static bool operator !=(AssetObjectType<TAssetLib, TAsset> a, AssetObjectType<TAssetLib, TAsset> b) => a.name != b.name;
    public static implicit operator bool(AssetObjectType<TAssetLib, TAsset> a)
    {
        return a.name != null;
    }

    public override bool Equals(object obj)
    {
        /*var type = obj as AssetObjectType<TAssetLib, TAsset>;
        if (this == null)
            Debug.Log("Fuck");
        return type != null &&
               name == type.name;*/
        if (obj == null)
            return false;
        if (!(obj is AssetObjectType<TAssetLib, TAsset>))
            return false;
        var type = obj as AssetObjectType<TAssetLib, TAsset>;
        return type.name == name;
    }

    public override int GetHashCode()
    {
        return 363513814 + EqualityComparer<string>.Default.GetHashCode(name);
    }
}
