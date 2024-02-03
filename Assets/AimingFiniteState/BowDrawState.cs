using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowDrawState : AimBaseState
{
    public override void EnterState(AimStateManager token)
    {
        if (token.isEquip) token.targetFOV = token.drawFOV;
        else token.targetFOV = token.normalFOV;
    }

    public override void UpdateState(AimStateManager token)
    {
        if (Input.GetKey(KeyCode.Mouse1) && token.equipWieght == 1) ExitState(token,token.aimToken);
    }
    void ExitState(AimStateManager token, AimBaseState state)
    {
        token.SwitchState(state);
    }
}
