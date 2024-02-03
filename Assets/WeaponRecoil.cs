using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public Transform recoilPos;
    [SerializeField] float kickBackAmount = 1;
    [SerializeField] float kickBackSpeed = 10 , returnSpeed = 20;
    float currentRecoilPos, finalRecoilPos;

    // Start is called before the first frame update
    void Update()
    {
        currentRecoilPos = Mathf.Lerp(currentRecoilPos,0, returnSpeed * Time.deltaTime);
        finalRecoilPos = Mathf.Lerp(finalRecoilPos,currentRecoilPos, kickBackSpeed * Time.deltaTime);
        recoilPos.localPosition = new Vector3(0,0, finalRecoilPos);
    }

    // Update is called once per frame
    public void TriggerRecoil() => currentRecoilPos -= kickBackAmount;

}
