using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon Data", fileName = "New Weapon")]
public class WeaponSO : ScriptableObject
{
    public new string name = "";
    public float weaponDamage = 0;
    public int currentAmmo = 0;
    public int currentAmmoLimit = 0;
    public int ammoPool = 0;
}
