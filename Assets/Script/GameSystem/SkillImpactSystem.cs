using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SkillImpactSystem : AssetsLib<SkillImpactSystem, GameObject>
{
    [Serializable]
    public class AssetDictionary : AssetDictionaryBase { }

    public class AssetObject : AssetObjectBase { }

    [SerializeField]
    [HideInInspector]
    AssetDictionary assetLib = new AssetDictionary();
    public override SerializableDictionary<string, GameObject> AssetsLibrary => assetLib;
}