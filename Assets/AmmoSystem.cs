using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSystem : MonoBehaviour
{
    public int currentAmmo;
    public int magSize;
    public int extraAmmo;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magSize;
    }
    private void Update()
    {
        //if (Input.GetKey(KeyCode.R)) Reload();
    }

    // Update is called once per frame
    public void Reload()
    {
        int refillAmount = magSize - currentAmmo;
        if (extraAmmo >= refillAmount)
        {
            extraAmmo -= refillAmount;
            currentAmmo += refillAmount;
        }
        else
        {
            currentAmmo += extraAmmo;
            extraAmmo = 0;
        }
    }

    public bool ShouldReload()
    {
        if (currentAmmo < magSize)
        {
            if (extraAmmo == 0) return false;
            else return true;
        }
        return false;
    }
}
