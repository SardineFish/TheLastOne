using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayerInputManager : Singleton<PlayerInputManager>
{
    public Entity PlayerInControl;

    public PlayerController PlayerController;

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
        PlayerController = PlayerInControl.GetComponent<PlayerController>();
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
        PlayerController.Move(MovementInput.GetMovement());
        PlayerController.FaceTo(InputManager.Instance.MouseOnGround() - PlayerInControl.transform.position);
        //SkillController.ActivateMovementSkill(MovementInput.GetMovement());
        
    }


}