using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Pickup Data", fileName = "New Pickup")]
public class PickupsSO : ScriptableObject
{
    public enum PickupFunctions
    {
        AmmoSupply,
        HealthSupply,
        ArmorSupply,
        GadgetSupply,
        None
    };
    [Header("Generic Attributes")]
    public string pickupName = "";
    public PickupFunctions pickupFunctionType = PickupFunctions.None;

    [Space]
    [Header("Ammo Supply Attributes")]
    public int ammoSupplyAmount = 0;
    //[Tooltip("Makes the pickup supply the player's in-magazine ammo first")] public bool prioritizeSupplyMagazineAmmo = false;

    [Space]
    [Header("Health Supply Attributes")]
    public float healthSupplyAmount = 0f;
    //[Tooltip("Makes the pickup supply the player's armor if health is already full")] public bool supplyArmorIfHealthFull = false;

    [Space]
    [Header("Armor Supply Attributes")]
    public float armorSupplyAmount = 0f;
    //[Tooltip("Makes the pickup supply the player's health if armor is already full")] public bool supplyHealthIfArmorFull = false;

    [Space]
    [Header("Gadget Supply Attributes")]
    public int gadgetSupplyAmount = 0;
}
