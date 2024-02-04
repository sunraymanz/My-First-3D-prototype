using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager token)
    {
        token.animToken.SetBool("Running", true);
        token.moveSpeed = token.runSpeed;
    }
    public override void UpdateState(MovementStateManager token)
    {
        if (token.dir.magnitude < 0.1f) ExitState(token, token.idleToken);
        else if (!Input.GetKey(KeyCode.LeftShift)) ExitState(token, token.walkToken);
        if (Input.GetKeyDown(KeyCode.Space) && token.isGround) token.SwitchState(token.jumpToken);
    }
    void ExitState(MovementStateManager token, MovementBaseState state)
    {
        token.animToken.SetBool("Running", false);
        token.SwitchState(state);
    }
}
