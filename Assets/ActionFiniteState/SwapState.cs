using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapState : ActionBaseState
{
    public override void EnterState(ActionStateManager token)
    {
        token.animToken.SetTrigger("Swap");
        token.handIK.weight = 0;
    }

    public override void UpdateState(ActionStateManager token)
    {
        
    }
}
