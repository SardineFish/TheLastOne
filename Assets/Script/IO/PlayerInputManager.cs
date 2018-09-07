using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayerInputManager : Singleton<PlayerInputManager>
{
    public Entity PlayerInControl;

    public EntityController EntityController;

    public SkillController SkillController;

    public KeyCode InteractKey;

    public KeyCode WeaponSwitchKey;

    public List<KeyCode> SkillKeys = new List<KeyCode>();

    public MovementInput MovementInput;
    
    private void OnEnable()
    {
        MovementInput = GetComponent<MovementInput>();
        if (!MovementInput)
            MovementInput = GetComponentInChildren<MovementInput>();
        SkillController = PlayerInControl.GetComponent<SkillController>();
        EntityController = PlayerInControl.GetComponent<EntityController>();
    }

    public void Update()
    {
        if (!PlayerInControl)
            throw new Exception("A player is required to be controled.");
        for(var i=0;i<SkillKeys.Count;i++)
        {
            if(InputManager.Instance.GetKeyDown(SkillKeys[i]))
            {
                SkillController.ActivateSkill(i);
            }
        }
        if(InputManager.Instance.GetKeyDown(InteractKey))
        {
            SkillController.ActivateSkill<InteractSkill>();
        }
        if(InputManager.Instance.GetKeyDown(WeaponSwitchKey))
        {
            PlayerInControl.GetComponent<Equipments>().Switch();
        }
        EntityController.Move(MovementInput.GetMovement());
        EntityController.FaceTo(InputManager.Instance.MouseOnGround() - PlayerInControl.transform.position);
        //SkillController.ActivateMovementSkill(MovementInput.GetMovement());
        
    }


}