using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MovementInput
{
    public Player PlayerInControl;
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 Target;
    public float StopThreshold = 0.01f;

    private void OnEnable()
    {
        if (this.InputKeys == null)
            this.InputKeys = new KeyCode[1];
        else if (this.InputKeys.Length > 2)
            System.Array.Resize(ref this.InputKeys, 1);
    }

    public void Update()
    {
        if (!PlayerInControl)
            throw new System.Exception("Require a player to control.");
        if(InputManager.Current.GetKeyDown(InputKeys[0]))
        {
            Target = InputManager.Current.MouseOnGround();
            moveDirection = Target - PlayerInControl.transform.position;

        }
        if ((Target - PlayerInControl.transform.position).ToVector2XZ().magnitude <= StopThreshold)
            moveDirection = Vector3.zero;
        if(moveDirection!=Vector3.zero)
            moveDirection = Target - PlayerInControl.transform.position;
    }
    public override Vector2 GetMovement()
    {
        return moveDirection.ToVector2XZ().normalized;
    }

    public override void Interrupt()
    {
        moveDirection = Vector3.zero;
    }
}
