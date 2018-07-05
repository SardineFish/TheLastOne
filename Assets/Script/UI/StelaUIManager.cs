using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StelaUIManager : Singleton<StelaUIManager>
{
    public SkillData[] PlayerSkills = new SkillData[0];
    public UITemplateRenderer SkillActionPanel;
    public UITemplateRenderer SkillImpactPanel;
    public UITemplateRenderer SkillEffectPanel;
    public UITemplateRenderer PlayerSkillsPanel;

    StelaData stelaData;
    public StelaData StelaData
    {
        get { return stelaData; }
        set
        {
            stelaData = value;
            SkillActionPanel.DataSource = stelaData.SkillActions.Select(action => action.Asset);
            SkillImpactPanel.DataSource = stelaData.SkillImpacts.Select(impact => impact.Asset);
        }
    }

    public void Display()
    {
        enabled = true;
    }
    public void Close()
    {
        enabled = false;
    }
    public void Reload()
    {
        
    }

    public void UpdateUI()
    {
        var player = GameSystem.Instance.PlayerInControl as Player;
        var playerSkills = player.GetComponent<SkillController>().Skills.Where(skill => skill is ConfigurableSkill).ToArray();

    }
}