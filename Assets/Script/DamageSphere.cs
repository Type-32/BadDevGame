using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSphere : MonoBehaviour
{
    public float damage = 10f;
    public Rigidbody sphere;
    public GameObject impactEffect;
    private void OnCollisionEnter(Collision collision)
    {
        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
        if(player != null)
        {
            player.TakeDamageFromPlayer(damage, true);
            GameObject temp234 = Instantiate(impactEffect, transform);
            Destroy(temp234, 0.5f);
            Destroy(gameObject);
        }
        GameObject temp = Instantiate(impactEffect, transform);
        Destroy(temp, 10f);
        Destroy(gameObject,10f);
    }
}
