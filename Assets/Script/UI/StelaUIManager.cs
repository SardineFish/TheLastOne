using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StelaUIManager : Singleton<StelaUIManager>
{
    public bool Visible = false;
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
            SetDataSource(value.SkillActions, value.SkillImpacts);
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
        WeaponsPanel.GetComponent<SelectionGroup>().AddEventListener(SelectionGroup.EVENT_ON_SELECT_CHANGE, () => LoadSkillPreview());
        PlayerSkillsPanel.GetComponent<SelectionGroup>().AddEventListener(SelectionGroup.EVENT_ON_SELECT_CHANGE, () => SkillSelectedChange());
    }
    void Update()
    {
        if (PreviewSkill)
        {
            var previewPlayer = GameObject.FindGameObjectWithTag("PreviewPlayer");
            previewPlayer.GetComponent<SkillController>().ActivateSkill(0, previewPlayer.transform.position + previewPlayer.transform.forward * 2);
        }
        if (Visible)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }
        
    }
    public void Display()
    {
        UpdateUI();
        StelaUI.SetActive(true);
        Visible = true;
    }
    public void Close()
    {
        ApplySkill();
        StelaUI.SetActive(false);
        Visible = false;
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
        var skillAction = SkillActionPanel.GetComponent<SelectionGroup>().Selected ? 
            SkillActionPanel.GetComponent<SelectionGroup>().Selected.GetComponent<UITemplate>().DataSource as SkillAction 
            : 
            null;
        var skillImpact = SkillImpactPanel.GetComponent<SelectionGroup>().Selected ? 
            SkillImpactPanel.GetComponent<SelectionGroup>().Selected.GetComponent<UITemplate>().DataSource as SkillImpact 
            : 
            null;
        if (!skillAction || !skillImpact)
            return;
        skillData.SkillAction = SkillActionLib.Instance.GetAssetObject<SkillActionLib.AssetObject>(skillAction);
        skillData.SkillImpact = SkillImpactSystem.Instance.GetAssetObject<SkillImpactSystem.AssetObject>(skillImpact.gameObject);

        previewPlayer.GetComponent<Equipments>().SelectedIndex = WeaponsPanel.GetComponent<SelectionGroup>().SelectedIndex;

        previewPlayer.GetComponent<SkillController>().ClearSkills();
        var skill = previewPlayer.GetComponent<SkillController>().CreateSkill<ConfigurableSkill>();
        skill.SetSkillData(skillData);
        skill.ActivateMethod = ActivateMethod.Position;
        PreviewSkill = skill;
        skill.CoolDown = 1;

        PlayerSkills[PlayerSkillsPanel.GetComponent<SelectionGroup>().SelectedIndex] = skillData;
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

        StartCoroutine(WaitForLoad());
    }

    public void SkillSelectedChange()
    {

        // Place current skill component to the first.
        var skillData = PlayerSkills[PlayerSkillsPanel.GetComponent<SelectionGroup>().SelectedIndex];
        var skillActionList = StelaData.SkillActions.ToList();
        var skillImpactList = StelaData.SkillImpacts.ToList();
        if (skillData != null)
        {
            var skillAction = skillData.SkillAction;
            var skillImpact = skillData.SkillImpact;

            if (skillActionList.Contains(skillAction))
                skillActionList.Remove(skillAction);
            if (skillImpactList.Contains(skillImpact))
                skillImpactList.Remove(skillImpact);
            skillActionList.Insert(0, skillAction);
            skillImpactList.Insert(0, skillImpact);

            StartCoroutine(WaitToNextFrame(() =>
            {
                SkillActionPanel.GetComponent<SelectionGroup>().SelectedIndex = 0;
                SkillImpactPanel.GetComponent<SelectionGroup>().SelectedIndex = 0;
                Debug.Log("selected");
            }));
        }
        SetDataSource(skillActionList, skillImpactList);
    }

    public void SetDataSource(IList<SkillActionLib.AssetObject> skillActions,IList<SkillImpactSystem.AssetObject> skillImpacts)
    {
        SkillActionPanel.DataSource = skillActions.Select(action => action.Asset).ToArray();
        SkillImpactPanel.DataSource = skillImpacts.Select(impact => (impact.Asset as GameObject).GetComponent<SkillImpact>()).ToArray();
    }

    public void ApplySkill()
    {
        GameSystem.Instance.PlayerInControl.GetComponent<SkillController>().ClearSkills();
        PlayerSkills
            .NotNull()
            .ForEach((skill) =>
            {
                if(skill.SkillEffect==null || skill.SkillEffect.Length == 0)
                {
                }
                var confSkill = GameSystem.Instance.PlayerInControl.GetComponent<SkillController>().CreateSkill<ConfigurableSkill>();
                confSkill.SetSkillData(skill);
            });
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        WeaponsPanel.GetComponent<SelectionGroup>().SelectedIndex = 0;
        PlayerSkillsPanel.GetComponent<SelectionGroup>().SelectedIndex = 0;
        //previewPlayer.GetComponent<Equipments>().SelectedIndex = WeaponsPanel.GetComponent<SelectionGroup>().SelectedIndex;
    }

    IEnumerator WaitToNextFrame(Action action)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
}