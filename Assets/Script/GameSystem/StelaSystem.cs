using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StelaSystem : Singleton<StelaSystem>
{
    public StelaData GenerateStelaData()
    {
        var stelaData = new StelaData();
        stelaData.SkillActions = SkillActionLib.Instance.AssetsLibrary.Values
            .WeightedRandomTake(3)
            .Select(action => SkillActionLib.Instance.GetAssetObject<SkillActionLib.AssetObject>(action))
            .ToArray();
        stelaData.SkillActions.ForEach((action) =>
        {
            var allAvailableImpact = SkillImpactSystem.Instance.AssetsLibrary.Values
            .Select(obj => (obj as GameObject).GetComponent<SkillImpact>())
            .Where(impact => (impact.ImpactType & action.Asset.AvailableImpactType) == impact.ImpactType)
            .ToList();

            stelaData.SkillImpactDict[action] = allAvailableImpact
            .WeightedRandomTake(Mathf.CeilToInt(allAvailableImpact.Count * 0.6f))
            .Select(impact => SkillImpactSystem.Instance.GetAssetObject<SkillImpactSystem.AssetObject>(impact.gameObject))
            .ToArray();
        });
        //var l = SkillImpactSystem.Instance.AssetsLibrary.Values.Select(x => (x as GameObject).GetComponent<SkillImpact>()).RandomTake(3).ToArray();
        /*
        stelaData.SkillImpacts = SkillImpactSystem.Instance.AssetsLibrary.Values
            .Select(obj => (obj as GameObject).GetComponent<SkillImpact>())
            .WeightedRandomTake(3)
            .Select(impact=>SkillImpactSystem.Instance.GetAssetObject<SkillImpactSystem.AssetObject>(impact.gameObject))
            .ToArray();*/
        return stelaData;
    }
}