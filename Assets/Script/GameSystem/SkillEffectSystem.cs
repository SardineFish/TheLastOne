using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SkillEffectSystem: AssetsLib<SkillEffectSystem,SkillEffect>
{
    [Serializable]
    public class AssetDictionary : AssetDictionaryBase { }

    [Serializable]
    public class AssetObject : AssetObjectBase { }

    [SerializeField]
    [HideInInspector]
    AssetDictionary assetLib = new AssetDictionary();

    public override SerializableDictionary<string, SkillEffect> AssetsLibrary => assetLib;
}