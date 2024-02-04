using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager token)
    {
        token.animToken.SetBool("Walking",true);
        token.moveSpeed = token.walkspeed;
    }
    public override void UpdateState(MovementStateManager token)
    {
        if (token.dir.magnitude < 0.1f) ExitState(token,token.idleToken);
        else if (Input.GetKeyDown(KeyCode.C)) ExitState(token, token.crouchToken);
        else if (Input.GetKey(KeyCode.LeftShift)) ExitState(token, token.runToken);
        if (Input.GetKeyDown(KeyCode.Space) && token.isGround) token.SwitchState(token.jumpToken);
    }

    void ExitState(MovementStateManager token , MovementBaseState state) 
    {
        token.animToken.SetBool("Walking", false);
        token.SwitchState(state);
    }
}
