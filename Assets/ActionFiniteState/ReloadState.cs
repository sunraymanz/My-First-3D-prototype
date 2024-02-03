using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : ActionBaseState
{
    // Start is called before the first frame update
    public override void EnterState(ActionStateManager token)
    {
        token.animToken.SetTrigger("Reload");
    }

    public override void UpdateState(ActionStateManager token)
    {

    }
    void ExitState(ActionStateManager token, ActionBaseState state)
    {
        token.SwitchState(state);
    }
}
