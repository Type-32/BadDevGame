using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public WeaponSwitching holder;
    public float pickRange = 5f;
    private void Start()
    {
        holder = FindObjectOfType<WeaponSwitching>();        
    }
    void Update()
    {
        if(Input.GetKeyDown("f") && holder.weaponCount < 2){
            
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, pickRange)){
                Debug.Log(hit.transform.name);
                GunPickup target = hit.transform.GetComponent<GunPickup>();
                if(target != null) target.PickupGun();
            }
            holder.SelectWeapon();
        }
    }
}
