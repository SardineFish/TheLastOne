using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerInputManager : Singleton<PlayerInputManager>
{
    public Entity PlayerInControl;

    public EntityController EntityController;

    public SkillController SkillController;

    public List<KeyCode> SkillKeys = new List<KeyCode>();

    public MovementInput MovementInput;

    [ExecuteInEditMode]
    private void OnEnable()
    {
        MovementInput = GetComponent<MovementInput>();
        if (!MovementInput)
            MovementInput = GetComponentInChildren<MovementInput>();
        
    }

    public void Update()
    {
        if (!PlayerInControl)
            throw new Exception("A player is required to be controled.");
        SkillController = PlayerInControl.GetComponent<SkillController>();
        EntityController = PlayerInControl.GetComponent<EntityController>();
        if(!SkillController)
        {
            SkillController = PlayerInControl.GetComponent<SkillController>();
            EntityController = PlayerInControl.GetComponent<EntityController>();
        }
        for(var i=0;i<SkillKeys.Count;i++)
        {
            if(InputManager.Current.GetKeyDown(SkillKeys[i]))
            {
                SkillController.ActivateSkill(i);
            }
        }
        SkillController.ActivateMovementSkill(MovementInput.GetMovement());
    }


}