using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingState : AimBaseState
{
    public override void EnterState(AimStateManager token)
    {
        token.animToken.SetBool("Aiming", true);
        token.bodyWieght = 0.7f;
        token.handWieght = 1f;
        token.targetFOV = token.aimFOV;
    }

    public override void UpdateState(AimStateManager token)
    {
        if (token.GetComponent<ActionStateManager>().currentState == token.GetComponent<ActionStateManager>().swapToken)
        token.handIK.weight = 0;
        else token.handIK.weight = Mathf.Lerp(token.handIK.weight, token.handWieght, 20 * Time.deltaTime);
        if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.E)) 
        ExitState(token, token.bowDrawToken);  
    }
    void ExitState(AimStateManager token, AimBaseState state)
    {
        token.handIK.weight = 0;
        token.bodyWieght = 0f;
        token.handWieght = 0f;
        token.animToken.SetBool("Aiming", false);
        token.SwitchState(state);
    }
}
