using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStateManager : MonoBehaviour
{
    public ActionBaseState currentState;
    public ActionIdleState idleToken = new ActionIdleState();
    public ReloadState reloadToken = new ReloadState();

    public WeaponManager currentWeapon;
    public Animator animToken;
    public AmmoSystem ammoToken;
    // Start is called before the first frame update
    void Start()
    {
        animToken = GetComponent<Animator>();
        currentWeapon = GetComponentInChildren<WeaponManager>();
        currentState = idleToken;
        SwitchState(idleToken);
    }

    public void SetCurrentWeapon(WeaponManager weaponToken)
    {
        currentWeapon = weaponToken;
        ammoToken = currentWeapon.GetComponent<AmmoSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void FinishReload() 
    {
        ammoToken.Reload();
        SwitchState(idleToken);
    }
}
