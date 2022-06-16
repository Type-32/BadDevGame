using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCube : MonoBehaviour
{
    public float damage = 10f;
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
        if(player != null)
        {
            player.TakeDamageFromPlayer(damage, true);
        }
    }
}
