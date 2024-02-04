using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager token)
    {

    }
    public override void UpdateState(MovementStateManager token)
    {
        if (token.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) token.SwitchState(token.runToken);
            else token.SwitchState(token.walkToken); 
        }
        if (Input.GetKeyDown(KeyCode.Space) && token.isGround) token.SwitchState(token.jumpToken);
        if (Input.GetKeyDown(KeyCode.C)) token.SwitchState(token.crouchToken);
    }
}
