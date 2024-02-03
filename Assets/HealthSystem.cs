using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health;
    RagdollManager ragdollToken;
    private void Start()
    {
        ragdollToken = GetComponent<RagdollManager>();
    }
    public void TakeDamnage(int dmg) 
    {
        health -= dmg;
        Debug.Log(gameObject.name + " Recieve " +dmg+"p : "+health+" HP Remain.");
        if (health <= 0) ObjectDeath();
    }

    public void ObjectDeath()
    {
        health = 0;
        ragdollToken.TriggerRagdoll();
        Debug.Log(gameObject.name + " is Dead.");
    }

}
