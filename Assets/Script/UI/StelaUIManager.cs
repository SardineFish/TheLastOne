using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StelaUIManager : Singleton<StelaUIManager>
{
    public SkillData[] PlayerSkills = new SkillData[0];
    public UITemplateRenderer SkillActionPanel;
    public UITemplateRenderer SkillImpactPanel;
    public UITemplateRenderer SkillEffectPanel;
    public UITemplateRenderer PlayerSkillsPanel;
    public Skill PreviewSkill;

    StelaData stelaData;
    public StelaData StelaData
    {
        get { return stelaData; }
        set
        {
            stelaData = value;
            SkillActionPanel.DataSource = stelaData.SkillActions.Select(action => action.Asset).ToArray();
            SkillImpactPanel.DataSource = stelaData.SkillImpacts.Select(impact => (impact.Asset as GameObject).GetComponent<SkillImpact>()).ToArray();
        }
    }

    void Start()
    {
        LoadPreviewScene();
        SkillActionPanel.GetComponent<SelectionGroup>().AddEventListener<SelectionButton>(SelectionGroup.EVENT_ON_SELECT_CHANGE, (selection) =>
        {
            LoadSkillPreview();
        });
        SkillImpactPanel.GetComponent<SelectionGroup>().AddEventListener(SelectionGroup.EVENT_ON_SELECT_CHANGE, () =>
         {
             LoadSkillPreview();
         });
    }
    void Update()
    {
        PreviewSkill?.Activate();   
    }
    public void Display()
    {
        UpdateUI();
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Reload()
    {
        
    }
    public void LoadPreviewScene()
    {
        SceneManager.LoadScene("SkillPreview", LoadSceneMode.Additive);
    }
    public void LoadSkillPreview()
    {
        var previewPlayer = GameObject.FindGameObjectWithTag("PreviewPlayer");
        if (!previewPlayer)
        {
            LoadPreviewScene();
            previewPlayer = GameObject.FindGameObjectWithTag("PreviewPlayer");
        }
        var skillData = new SkillData();
        var skillAction =  SkillActionPanel.GetComponent<SelectionGroup>().Selected?.GetComponent<UITemplate>().DataSource as SkillAction;
        var skillImpact = SkillImpactPanel.GetComponent<SelectionGroup>().Selected?.GetComponent<UITemplate>().DataSource as SkillImpact;
        if (!skillAction || !skillImpact)
            return;
        skillData.SkillAction = SkillActionLib.Instance.GetAssetObject(skillAction);
        skillData.SkillImpact = SkillImpactSystem.Instance.GetAssetObject(skillImpact.gameObject);

        previewPlayer.GetComponent<SkillController>().ClearSkills();
        var skill = previewPlayer.GetComponent<SkillController>().CreateSkill<ConfigurableSkill>();
        skill.SetSkillData(skillData);
        PreviewSkill = skill;
        skill.CoolDown = 1;
    }

    public void UpdateUI()
    {
        var player = GameSystem.Instance.PlayerInControl as Player;
        var playerSkills = player.GetComponent<SkillController>().Skills.Where(skill => skill is ConfigurableSkill).ToArray();

    }
}