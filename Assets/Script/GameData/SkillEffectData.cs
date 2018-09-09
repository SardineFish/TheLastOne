using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SkillEffectData:IDocumented
{
    public SkillEffectSystem.AssetObject SkillEffect;
    public float Multiple = 1;
    public Sprite Icon => SkillEffect.Asset.Icon;

    public string Name => SkillEffect.Asset.Name;
    public string DisplayName => SkillEffect.Asset.DisplayName;

    public string Description => SkillEffect.Asset.Description;
    /*public SkillEffectData()
    {
        Multiple = 1;
        SkillEffect = new SkillEffectSystem.AssetObject();
    }*/
    public SkillEffectData(SkillEffectSystem.AssetObject skillEffectAssetObject, float multiple = 1)
    {
        SkillEffect = skillEffectAssetObject;
        Multiple = multiple;
    }
    public SkillEffectData()
    {

    }
}