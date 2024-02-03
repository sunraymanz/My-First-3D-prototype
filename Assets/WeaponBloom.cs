using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField] float defaultBloomAngle = 2;
    [SerializeField] float walkBloomMultiplier = 1.5f;
    [SerializeField] float crouchBloomMultiplier = 0.5f;
    [SerializeField] float sprintBloomMultiplier = 2f;
    [SerializeField] float adsBloomMultiplier =0.5f;

    MovementStateManager movementToken;
    AimStateManager aimingToken;

    public float currentBloom;

    // Start is called before the first frame update
    void Start()
    {
        movementToken = GetComponentInParent<MovementStateManager>();
        aimingToken = GetComponentInParent<AimStateManager>();
    }

    public Vector3 BloomAngle(Transform barrelPos)
    {
        if (movementToken.currentState == movementToken.idleToken) currentBloom = defaultBloomAngle;
        else if (movementToken.currentState == movementToken.walkToken) currentBloom = defaultBloomAngle * walkBloomMultiplier;
        else if (movementToken.currentState == movementToken.runToken) currentBloom = defaultBloomAngle * sprintBloomMultiplier;
        else if (movementToken.currentState == movementToken.crouchToken)
        {
            if (movementToken.dir.magnitude == 0) currentBloom = defaultBloomAngle * crouchBloomMultiplier;
            else currentBloom = defaultBloomAngle * crouchBloomMultiplier * walkBloomMultiplier;
        }

        if (aimingToken.currentState == aimingToken.aimToken) currentBloom *= adsBloomMultiplier;

        float randX = Random.Range(-currentBloom, currentBloom);
        float randY = Random.Range(-currentBloom, currentBloom);
        float randZ = Random.Range(-currentBloom, currentBloom);

        Vector3 randomRotaion = new Vector3(randX, randY, randZ);
        return barrelPos.localEulerAngles + randomRotaion;
    }
}
