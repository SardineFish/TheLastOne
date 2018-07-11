using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

[Serializable]
public class WeaponSystem : AssetsLib<WeaponSystem, GameObject>
{
    [SerializeField]
    public WeaponSystem.AssetObject obj;
    [Serializable]
    public class AssetDictionary : AssetDictionaryBase { }
    [Serializable]
    public class AssetObject : AssetObjectBase { }


    [SerializeField]
    [HideInInspector]
    private AssetDictionary assetsLibrary = new AssetDictionary();
    public override SerializableDictionary<string, GameObject> AssetsLibrary => assetsLibrary;

    private void Start()
    {
        obj = new AssetObject() { name = AssetsLibrary.Keys[0] };
 
    }
}
