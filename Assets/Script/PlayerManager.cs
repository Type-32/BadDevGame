using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Script Control")]
    public PlayerControls controls;
    public PlayerStats stats;
    public UIManager ui;
    public MouseLookScript cam;
    public HolderManager gunHolder;
    public GadgetUsageScript gadgetFunc;

    [Space]
    [Header("References")]
    public Rigidbody body;
    public CapsuleCollider capsuleCollider;
    public GameObject fpsCam;
    public GameObject deathCam;
    public Animator cameraAnim;

    [Space]
    [Header("Local Game Stats")]
    public int totalKilledEnemies = 0;
    public int totalDealtDamage = 0;
    public float totalAbsorbedDamage = 0;
    public int totalGainedXP = 0;

    private void Start()
    {
        gadgetFunc = FindObjectOfType<GadgetUsageScript>();
        gunHolder = FindObjectOfType<HolderManager>();
        ui = FindObjectOfType<UIManager>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ground") stats.onGround = true;
        stats.isJumping = false;
    }
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Ground") stats.onGround = false;
    }
    public void GetPickupsForPlayer()
    {
        RaycastHit detectRay;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out detectRay, 3f))
        {
            PickupScript tempPickup = detectRay.transform.GetComponent<PickupScript>();
            if (tempPickup != null)
            {
                if(tempPickup.pickupData.pickupFunctionType == PickupsSO.PickupFunctions.HealthSupply)
                {
                    if (tempPickup.pickupData.pickupName == "Small Supply Pack") gadgetFunc.smallHealthSupplyPack.GetComponent<Animator>().Play("SmallPackUse");
                    if (tempPickup.pickupData.healthSupplyAmount + stats.health > stats.healthLimit)
                    {
                        stats.health = stats.healthLimit;
                    }
                    else
                    {
                        stats.health += tempPickup.pickupData.healthSupplyAmount;
                    }
                    Destroy(tempPickup.gameObject);
                }
                if (tempPickup.pickupData.pickupFunctionType == PickupsSO.PickupFunctions.ArmorSupply)
                {
                    if (tempPickup.pickupData.armorSupplyAmount + stats.armor > stats.armorLimit)
                    {
                        stats.armor = stats.armorLimit;
                    }
                    else
                    {
                        stats.armor += tempPickup.pickupData.armorSupplyAmount;
                    }
                    Destroy(tempPickup.gameObject);
                }
            }
        }
    }
    public void SetMouseSensitivity(float value){
        stats.mouseSensitivity = value;
        controls.aimingMouseSensitivity = stats.mouseSensitivity * 0.8f;
    }
    public void TakeDamageFromPlayer(float amount, bool considerArmor)
    {
        if (considerArmor)
        {
            if (stats.armor > 0)
            {
                stats.health -= amount * stats.armorResistance;
                stats.armor -= amount * (1 - stats.armorResistance);
                totalAbsorbedDamage += amount * stats.armorResistance;
                totalAbsorbedDamage += amount * (1 - stats.armorResistance);
            }
            else
            {
                stats.health -= amount;
                totalAbsorbedDamage += amount;
            }
        }
        else
        {
            stats.health -= amount;
            totalAbsorbedDamage += amount;
        }
        if(stats.health <= 0f)
        {
            Cursor.lockState = CursorLockMode.None;
            Die();
            return;
        }
    }
    private void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Death Here ");
        GameObject tmp = Instantiate(deathCam, fpsCam.transform.position, fpsCam.transform.rotation);
        tmp.gameObject.GetComponentInChildren<DeathCamLookScript>().InitializeOnPlayerDeath(stats.mouseSensitivity, stats.ClampCamRotX, stats.ClampCamRotZ, stats.mouseMovementEnabled, stats.invertedMouse);
        ui.anim.SetTrigger("PlayerKilled");
        ui.deathMessageStats.text = "Total Enemy Kill Count: " + totalKilledEnemies.ToString() + "\nTotal Gained XP: " + totalGainedXP.ToString() + "\n Total Dealt Damage: " + totalDealtDamage.ToString() + "\nTotal Absorbed Damage: " + totalAbsorbedDamage.ToString() + "\nSurvived To Round: " + ui.roundCount.ToString();
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.None;
        Destroy(gameObject);
        Cursor.lockState = CursorLockMode.None;
        return;
    }
}
