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

    public class AssetObject : AssetObjectBase {
        public AssetObject() : base() { }
        public AssetObject(string name) : base(name) { }
    }

    [SerializeField]
    [HideInInspector]
    AssetDictionary assetLib = new AssetDictionary();
    public override SerializableDictionary<string, GameObject> AssetsLibrary => assetLib;

    public SkillImpactSystem()
    {
        Enum.GetValues(typeof(ImpactType)).Cast<ImpactType>().ForEach(impact => assetLib.Add(impact.ToString(), null));
    }

    public AssetObject GetAssetObject(GameObject obj)
    {
        return new AssetObject(AssetsLibrary.KeyOf(obj));
    }
}