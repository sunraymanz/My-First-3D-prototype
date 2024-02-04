using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    public int damage = 10;
    public int kickForce = 100;
    AimStateManager aimToken;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;
    AmmoSystem ammoToken;

    ActionStateManager actionToken;
    WeaponRecoil recoilToken;
    WeaponSystem weaponSystemToken;

    ParticleSystem vfxToken;
    Light muzzleLightToken;
    [SerializeField] float lightIntensity = 1;
    [SerializeField] float lightDimSpeed = 20;

    WeaponBloom bloomToken;
    public Transform hintIK,targetIK;

    // Start is called before the first frame update
    private void Start()
    {
        aimToken = GetComponentInParent<AimStateManager>();       
        actionToken = GetComponentInParent<ActionStateManager>();       
        vfxToken = GetComponentInChildren<ParticleSystem>();
        muzzleLightToken = GetComponentInChildren<Light>();
    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        ammoToken = GetComponent<AmmoSystem>();
        bloomToken = GetComponent<WeaponBloom>();
        recoilToken = GetComponent<WeaponRecoil>();
        muzzleLightToken.intensity = 0;
        if (GetComponentInParent<WeaponSystem>())
        {
            weaponSystemToken = GetComponentInParent<WeaponSystem>();
            weaponSystemToken.SetCurrentWeapon(this);
            recoilToken.recoilPos = weaponSystemToken.recoilPos;
        }
    }
    void Update()
    {
        if (ShouldFire()) Fire();
        Debug.Log("Ammo : "+ammoToken.currentAmmo);
        muzzleLightToken.intensity = Mathf.Lerp(muzzleLightToken.intensity, 0, lightDimSpeed*Time.deltaTime);
    }

    bool ShouldFire()
    {
        if (fireRateTimer < 2f) fireRateTimer += Time.deltaTime;
        if (actionToken.currentState == actionToken.reloadToken) return false;
        if (actionToken.currentState == actionToken.swapToken) return false;
        if (fireRateTimer < fireRate) return false;
        if (ammoToken.currentAmmo == 0) return false;
        if (actionToken.animToken.GetLayerWeight(1) > 0.1f && actionToken.animToken.GetBool("Aiming"))
        {
            if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
            if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        }
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aimToken.aimPos);
        barrelPos.localEulerAngles = bloomToken.BloomAngle(barrelPos);
        for (int i = 0; i < bulletsPerShot; i++)
        {
            audioSource.PlayOneShot(gunShot);
            recoilToken.TriggerRecoil();
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            currentBullet.GetComponent<Bullet>().weaponToken = this;
            currentBullet.GetComponent<Bullet>().dirFire = barrelPos.forward;
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
            TriggerMuzzleFlash();
            ammoToken.currentAmmo -= 1;
        }
    }

    void TriggerMuzzleFlash() 
    {
        muzzleLightToken.intensity = lightIntensity;
        vfxToken.Play();
    }
}
