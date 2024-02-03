using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponSystem : MonoBehaviour
{
    ActionStateManager actionToken;
    public TwoBoneIKConstraint handIKToken;
    public Transform recoilPos;

    // Start is called before the first frame update
    public void SetCurrentWeapon(WeaponManager weaponToken) 
    {
        if (actionToken == null) actionToken = GetComponent<ActionStateManager>();
        actionToken.SetCurrentWeapon(weaponToken);
        handIKToken.data.hint = weaponToken.hintIK;
        handIKToken.data.target = weaponToken.targetIK;
    }
}
