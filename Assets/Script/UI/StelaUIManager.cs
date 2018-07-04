using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StelaUIManager : Singleton<StelaUIManager>
{
    public SkillAction[] SkillActions = new SkillAction[0];
    public SkillImpact[] SkillImpacts = new SkillImpact[0];
    public SkillEffect[] SkillEffects = new SkillEffect[0];
    public UITemplateRenderer SkillActionPanel;
    public UITemplateRenderer SkillImpactPanel;
    public UITemplateRenderer SkillEffectPanel;

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

    public void SetSkillComponents(SkillAction[] skillActions, SkillEffect[] skillEffects, SkillImpact[] skillImpacts)
    {
        SkillActions = skillActions;
        SkillEffects = skillEffects;
        SkillImpacts = skillImpacts;
        SkillActionPanel.DataSource = SkillActions;
        SkillImpactPanel.DataSource = SkillImpacts;
        SkillEffectPanel.DataSource = SkillImpacts;
    }
}