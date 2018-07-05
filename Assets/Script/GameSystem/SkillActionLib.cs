using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SkillActionLib : AssetsLib<SkillActionLib, SkillAction>
{
    [Serializable]
    public class AssetDictionary : AssetDictionaryBase { }

    [SerializeField]
    [HideInInspector]
    private AssetDictionary assetLib = new AssetDictionary();
    public override SerializableDictionary<string, global::SkillAction> AssetsLibrary => assetLib;
}