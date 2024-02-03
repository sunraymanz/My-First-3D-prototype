using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager token)
    {
        token.animToken.SetTrigger("Jumping");
    }

    public override void UpdateState(MovementStateManager token)
    {
        token.animToken.SetBool("OnJumping", token.onJumping);
        if (token.dir.magnitude > 0.1f) token.animToken.SetBool("Walking", true);
        else token.animToken.SetBool("Walking", false);
        if (token.onJumping && token.isGround)
        {
            if (Input.GetKey(KeyCode.LeftShift)) ExitState(token, token.runToken);
            else if (token.dir.magnitude > 0.1f) ExitState(token, token.walkToken);
            else if (token.animToken.GetBool("Crouching")) ExitState(token, token.crouchToken);
            else ExitState(token,token.idleToken);
        }
    }
    void ExitState(MovementStateManager token, MovementBaseState state)
    {
        token.onJumping = false;
        token.animToken.SetBool("OnJumping", false);
        token.animToken.SetBool("Walking", false);
        token.animToken.SetBool("Running", false);
        token.SwitchState(state);
    }
}
