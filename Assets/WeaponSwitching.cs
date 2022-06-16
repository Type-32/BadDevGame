using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public int weaponCount = 0;
    public List<GameObject> weaponList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedWeapon >= 2) selectedWeapon = 1;
        if(selectedWeapon < 0) selectedWeapon = 0;
        int previousSelectedWeapon = selectedWeapon;
        if (selectedWeapon > weaponCount) selectedWeapon = 0;
        weaponCount = transform.childCount;
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && !transform.GetComponentInChildren<GunStats>().isReloading)
        {
            if(selectedWeapon >= transform.childCount - 1) selectedWeapon = 0;
            else selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !transform.GetComponentInChildren<GunStats>().isReloading)
        {
            if (selectedWeapon <= 0) selectedWeapon = transform.childCount - 1;
            else selectedWeapon--;
        }
        if (selectedWeapon != previousSelectedWeapon) SelectWeapon();
    }
    public void SelectWeapon()
    {
        
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon && weapon != null) weapon.gameObject.SetActive(true);
            else weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
