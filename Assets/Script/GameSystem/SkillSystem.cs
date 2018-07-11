using UnityEngine;
using System.Collections;
using System;

public class SkillSystem : AssetsLib<SkillSystem, SkillAction>
{
    [Serializable]
    public class AssetObject : AssetObjectBase { }
    [Serializable]
    public class AssetDictionary : AssetDictionaryBase { }


    [SerializeField]
    [HideInInspector]
    private AssetDictionary assetLib = new AssetDictionary();
    public override SerializableDictionary<string, SkillAction> AssetsLibrary => assetLib;
}