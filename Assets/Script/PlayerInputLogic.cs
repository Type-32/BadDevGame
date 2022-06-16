using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputLogic : MonoBehaviour
{
    public PlayerManager player;
    public PlayerStats stats;
    private float slideDurationCounter = 0f;
    private float slideDurationLimit;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    };
    private void Start()
    {
        slideDurationCounter = slideDurationLimit = stats.slideDuration;
    }
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) stats.isWalking = true;
        else stats.isWalking = false;

        if (!stats.toggleAiming)
        {
            if (Input.GetButton("Fire2")) stats.isAiming = true;
            else stats.isAiming = false;
        }
        else
        {
            if (Input.GetButtonDown("Fire2") && !stats.isAiming) stats.isAiming = true;
            else if (Input.GetButtonDown("Fire2") && stats.isAiming) stats.isAiming = false;
        }
        if (Input.GetKeyDown("c"))
        {

            if (stats.isSprinting)
            {

                if (slideDurationCounter >= slideDurationLimit && stats.onGround)
                {
                    slideDurationCounter = 0f;
                    stats.isCrouching = true;
                    stats.isSliding = true;
                }
            }
            else
            {
                if (!stats.isCrouching)
                {
                    stats.isCrouching = true;
                }
                else
                {
                    stats.isCrouching = false;
                }
            }
        }

        if (slideDurationCounter < slideDurationLimit && stats.isSliding)
        {
            if (!stats.isCrouching)
            {
                slideDurationCounter = slideDurationLimit;
                stats.isSliding = false;
                stats.onGround = true;
            }
            slideDurationCounter += Time.deltaTime;
        }
        else
        {
            if (stats.isSliding)
            {
                stats.isSliding = false;
                stats.onGround = true;
                Debug.Log("Slide Finished");
            }
        }
        //player.cameraAnim.SetBool("isSliding", stats.isSliding);
        //player.cameraAnim.SetBool("isCrouching", stats.isCrouching);

        if (Input.GetKey("left shift") && !stats.isAiming && !stats.isCrouching && stats.isWalking) stats.isSprinting = true;
        else stats.isSprinting = false;

        if (Input.GetKey("space") && stats.onGround) stats.isJumping = true;
        else stats.isJumping = false;
    }
}
