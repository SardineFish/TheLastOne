using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StelaSystem : Singleton<StelaSystem>
{
    public StelaData GenerateStelaData()
    {
        var stelaData = new StelaData();
        stelaData.SkillActions = SkillActionLib.Instance.AssetsLibrary.Values
            .RandomTake(3)
            .Select(action => SkillActionLib.Instance.GetAssetObject(action))
            .ToArray();
        stelaData.SkillImpacts = SkillImpactSystem.Instance.AssetsLibrary.Values
            .Select(obj => (obj as GameObject).GetComponent<SkillImpact>())
            .RandomTake(3)
            .Select(impact=>SkillImpactSystem.Instance.GetAssetObject(impact))
            .ToArray();
        return stelaData;
    }
}