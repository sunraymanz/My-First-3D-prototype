using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] ActionStateManager actionToken;
    public TwoBoneIKConstraint handIKToken;
    public Transform recoilPos;

    public WeaponManager[] weaponList;
    public int currentIndex = 0;

    private void Awake()
    {
        for (int i = 0; i < weaponList.Length; i++)
        {
            //if (i == 0) weaponList[i].gameObject.SetActive(true);
            //else 
            weaponList[i].gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    public void SetCurrentWeapon(WeaponManager weaponToken)
    {
        if (actionToken == null) actionToken = GetComponent<ActionStateManager>();
        actionToken.SetCurrentWeapon(weaponToken);
        handIKToken.data.hint = weaponToken.hintIK;
        handIKToken.data.target = weaponToken.targetIK;
    }

    public void ChangeWeapon(float mouseDir)
    {
        if (mouseDir > 0)
        {
            if (currentIndex == weaponList.Length - 1) currentIndex = 0;
            else currentIndex++;
        }
        else
        {
            if (currentIndex == 0) currentIndex = weaponList.Length - 1;
            else currentIndex--;
        }
    }

    public void WeaponUneqiup() => weaponList[currentIndex].gameObject.SetActive(false);
    public void WeaponSwap() => ChangeWeapon(actionToken.idleToken.mouseDir);
    public void WeaponEqiup() => weaponList[currentIndex].gameObject.SetActive(true);
    public void Swapped() => actionToken.SwitchState(actionToken.idleToken);
}
