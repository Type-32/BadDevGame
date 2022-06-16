using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStats : MonoBehaviour
{
    //public WeaponSO weaponData;
    public enum WeaponFireTypes
    {
        FullyAutomatic,//Auto + Single
        SemiAutomatic,//Auto + Semi + Single
        BurstSingle,//Burst + Single
        SingleShot,//Single
        SniperSingle//Single |++| BoltAnimation
    };
    [Header("Generic Attributes")]
    public WeaponFireTypes weaponFireType;
    [Range(0f,200f)] public float damage = 20f;
    public float range = 100f;
    public float impactForce = 3f;
    public float reloadTime = 3f;
    [Range(5, 20)] public float throwForce = 8f;
    public bool autoReload = false;
    public float aimSpeed = 10f;
    [Range(1f, 1.5f)] public float FOVMultiplier = 1.1f;

    [Space]
    [Header("Ammo & Stuff")]
    public int ammo = 30;
    public int maxAmmo = 30;
    public int ammoPool = 120;
    public int maxAmmoPool = 120;

    [Space]
    [Header("Automatic Shooting Statistics")]
    public float fireRate = 15f;
    [Range(0, 2)] public int availableFireModes = 1;

    [Space]
    [Header("SniperSingle Generic Statistics")]
    public float boltRecoveryDuration = 1.5f;

    [Space]
    [Header("Positional Weapon Sway")]
    public float swayIntensity = 0.05f;
    public float maxSwayIntensity = 0.15f;
    public float smoothness = 5f;
    public float aimSwayIntensity = 0.005f;

    [Space]
    [Header("Rotational Weapon Sway")]
    public float rotSwayIntensity = 1f;
    public float maxRotSwayIntensity = 2f;
    public float rotSmoothness = 5f;
    public float aimRotSwayIntensity = 0.05f;

    [Space]
    [Header("Gun Recoil Stats")]
    public float verticalRecoil;
    public float horizontalRecoil;
    public float sphericalShake;
    public float transitionalSnappiness = 5f;
    public float recoilReturnSpeed = 8f;

    [HideInInspector] public int selectedBarrelIndex = 0;
    [HideInInspector] public int selectedUnderbarrelIndex = 0;
    [HideInInspector] public int selectedSightIndex = 0;

    [HideInInspector] public bool isWalking = false;
    [HideInInspector] public bool isReloading = false;
    [HideInInspector] public bool isAiming = false;
    [HideInInspector] public bool isSprinting = false;
    [HideInInspector] public bool isShooting = false;
    [HideInInspector] public bool isAttaching = false;
}
