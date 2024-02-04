using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIdleState : ActionBaseState
{
    public float mouseDir;
    // Start is called before the first frame update
    public override void EnterState(ActionStateManager token) 
    {
        
    }

    public override void UpdateState(ActionStateManager token)
    {
        if (Input.GetKeyUp(KeyCode.R) && token.ammoToken.ShouldReload() && token.animToken.GetLayerWeight(1)>0.1) ExitState(token, token.reloadToken);
        else if (Input.mouseScrollDelta.y != 0 && token.animToken.GetLayerWeight(1) > 0.1)
        {
            mouseDir = Input.mouseScrollDelta.y;
            ExitState(token, token.swapToken);
        }    
    }
    void ExitState(ActionStateManager token, ActionBaseState state)
    {
        token.SwitchState(state);
    }
}
