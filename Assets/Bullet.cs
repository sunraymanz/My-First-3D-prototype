using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float destroyTime;
    public WeaponManager weaponToken;
    public Vector3 dirFire;
    // Start is called before the first frame update
    void Update()
    {
        Destroy(this.gameObject,destroyTime);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<HealthSystem>())
        {
            HealthSystem temp = collision.gameObject.GetComponentInParent<HealthSystem>();
            if (temp.health>0)
            {
                temp.TakeDamnage(weaponToken.damage);
                collision.rigidbody.AddForce(dirFire* weaponToken.kickForce,ForceMode.Impulse);
            }
        }
        Destroy(this.gameObject);
    }
}
