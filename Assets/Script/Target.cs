using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;
    public GameObject hitEffect;
    public GameObject deathEffect;
    //public GameObject healthText;
    //public GameObject maxHealthText;
    public void TakeDamage(float amount){
        health -= amount;
        if(hitEffect != null) Instantiate(hitEffect, transform.position, transform.rotation);
        if(health <= 0){
            if(deathEffect != null){
                GameObject tmp = Instantiate(deathEffect, transform.position, transform.rotation);
                Destroy(tmp, 2f);
            }
            Destroy(gameObject);
        }
    }
    /*
    void Update(){

    }*/
}
