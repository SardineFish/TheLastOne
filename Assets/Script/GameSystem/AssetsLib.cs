using UnityEngine;
using System.Collections;

public abstract class AssetsLib<TSingleton, TAsset> : Singleton<TSingleton>
    where TSingleton : AssetsLib<TSingleton, TAsset>
    where TAsset : UnityEngine.Object
{
    public SerializableDictionary<string, TAsset> AssetsLibrary = new SerializableDictionary<string, TAsset>();
    public virtual TAsset GetAsset(string name) => AssetsLibrary[name];
}
