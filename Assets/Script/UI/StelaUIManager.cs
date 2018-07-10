using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StelaUIManager : Singleton<StelaUIManager>
{
    public GameObject StelaUI;
    public SkillData[] PlayerSkills = new SkillData[0];
    public Weapon[] Weapons = new Weapon[2];
    public UITemplateRenderer SkillActionPanel;
    public UITemplateRenderer SkillImpactPanel;
    public UITemplateRenderer SkillEffectPanel;
    public UITemplateRenderer PlayerSkillsPanel;
    public UITemplateRenderer WeaponsPanel;
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
        if (PreviewSkill)
        {
            var previewPlayer = GameObject.FindGameObjectWithTag("PreviewPlayer");
            previewPlayer.GetComponent<SkillController>().ActivateSkill(0);
        }
    }
    public void Display()
    {
        UpdateUI();
        StelaUI.SetActive(true);
    }
    public void Close()
    {
        StelaUI.SetActive(false);
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
        /*if (!previewPlayer)
        {
            LoadPreviewScene();
            previewPlayer = GameObject.FindGameObjectWithTag("PreviewPlayer");
        }*/
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
        if (!GameObject.FindGameObjectWithTag("PreviewPlayer"))
        {
            LoadPreviewScene();
        }

        var previewPlayer = GameObject.FindGameObjectWithTag("PreviewPlayer");
        var player = GameSystem.Instance.PlayerInControl as Player;
        PlayerSkills = new SkillData[4];
        player.GetComponent<SkillController>().Skills
            .Where(skill => skill is ConfigurableSkill)
            .Select(skill => (skill as ConfigurableSkill).SkillData)
            .ToArray()
            .CopyTo(PlayerSkills, 0);
        PlayerSkillsPanel.GetComponent<UITemplateRenderer>().DataSource = PlayerSkills;

        Weapons = new Weapon[2];
        player.GetComponent<Equipments>().Items
            .Where(item => item.GetComponent<Weapon>())
            .Select(item => item.GetComponent<Weapon>())
            .ToArray()
            .CopyTo(Weapons, 0);
        WeaponsPanel.DataSource = Weapons;

        previewPlayer.GetComponent<Equipments>().Items = Weapons
            .Where(weapon => weapon)
            .Select(weapon => weapon.gameObject)
            .ToList();
        previewPlayer.GetComponent<Equipments>().SelectedIndex = WeaponsPanel.GetComponent<SelectionGroup>().SelectedIndex;
    }
}