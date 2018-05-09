using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AnimatorLib : AssetsLib<AnimatorLib, SkillAction>
{
    [Serializable]
    public class AssetDictionary : AssetDictionaryBase { }
    [Serializable]
    public class Animator : AssetObjectBase { }

    [SerializeField]
    [HideInInspector]
    private AssetDictionary assetLib = new AssetDictionary();
    public override SerializableDictionary<string, SkillAction> AssetsLibrary => assetLib;
}