using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager token)
    {
        token.animToken.SetBool("Crouching", true);
        token.moveSpeed = token.crouchSpeed;
    }
    public override void UpdateState(MovementStateManager token)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(token, token.runToken);
        else if (Input.GetKeyDown(KeyCode.C)) 
        {
            if (token.dir.magnitude < 0.1f) ExitState(token, token.idleToken);
            else ExitState(token, token.walkToken);
        }
    }
    void ExitState(MovementStateManager token, MovementBaseState state)
    {
        token.animToken.SetBool("Crouching", false);
        token.SwitchState(state);
    }
}
