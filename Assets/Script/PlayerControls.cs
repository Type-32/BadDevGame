using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    
    public PlayerManager player;
    private float speedValve = 0f;
    private Vector3 playerInput;
    public Vector3 MoveVec;
    private float normalFOV;
    private float sprintFOV;
    private float startYScale;
    private float capsuleColliderInitHeight;
    private float capsuleColliderCrouchHeight;
    public Vector3 cameraInitPos;
    public Vector3 cameraCrouchPos;
    public float aimingMouseSensitivity;
    private MouseLookScript mouseLook;
    private void Awake()
    {
        normalFOV = player.stats.cameraFieldOfView;
        sprintFOV = player.stats.sprintFOVMultiplier * player.stats.cameraFieldOfView;
    }
    private void Start()
    {
        mouseLook = player.fpsCam.GetComponent<MouseLookScript>();
        
        capsuleColliderInitHeight = player.capsuleCollider.height;
        capsuleColliderCrouchHeight = player.capsuleCollider.height/2;
        cameraInitPos = player.fpsCam.transform.position;
        cameraCrouchPos = player.fpsCam.transform.position;
        cameraCrouchPos = new Vector3 (cameraCrouchPos.x, 0, cameraCrouchPos.z);
        startYScale = transform.localScale.y;
    }
    void Update()
    {
        aimingMouseSensitivity = player.stats.mouseSensitivity * 0.8f;
        if (player.stats.isSprinting && !player.stats.isSliding) 
        {
            speedValve = player.stats.sprintSpeed;
        } 
        else if (player.stats.isSliding)
        {
            speedValve = Mathf.Lerp(player.stats.slideSpeed, player.stats.crouchSpeed, Time.deltaTime * player.stats.slideDuration * 1.5f);
        }
        else if (player.stats.isCrouching && !player.stats.isSliding)
        {
            speedValve = player.stats.crouchSpeed;
        }
        else 
        {
            speedValve = player.stats.speed;
        }

        if (Input.GetKeyDown("f")) player.GetPickupsForPlayer();

        if(player.stats.onGround && player.stats.isJumping){
            Debug.Log("Player Jumping ");
            player.body.AddForce(Vector3.up * player.stats.jumpForce, ForceMode.Impulse);
        }
        if(player.stats.playerMovementEnabled){
            playerInput = new Vector3(Input.GetAxis("Horizontal") * speedValve, player.body.velocity.y, Input.GetAxis("Vertical") * speedValve);
            MoveVec = transform.TransformDirection(playerInput);
            player.body.velocity = MoveVec;
        }
        if (player.stats.isSprinting && !player.stats.isSliding)
        {
            player.fpsCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(player.fpsCam.GetComponent<Camera>().fieldOfView, sprintFOV, player.stats.sprintFOVMultiplier * Time.deltaTime);
        }
        else if (player.stats.isSliding)
        {
            player.fpsCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(player.fpsCam.GetComponent<Camera>().fieldOfView, sprintFOV * 1.2f, player.stats.sprintFOVMultiplier * Time.deltaTime);
        }
        else
        {
            player.fpsCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(player.fpsCam.GetComponent<Camera>().fieldOfView, normalFOV, player.stats.sprintFOVMultiplier * Time.deltaTime);
        }

        if (player.stats.isAiming)
        {
            if (player.ui.entityIndicator.activeSelf) player.ui.entityIndicator.SetActive(false);
            mouseLook.mouseSensitivityValve = aimingMouseSensitivity;
        }
        else
        {
            RaycastHit detection;
            if (Physics.Raycast(player.fpsCam.transform.position, player.fpsCam.transform.forward, out detection, 50f))
            {
                EnemyAI enemy = detection.transform.GetComponent<EnemyAI>();
                PickupScript pickup = detection.transform.GetComponent<PickupScript>();
                if (enemy != null)
                {
                    if (!player.ui.entityIndicator.activeSelf) player.ui.entityIndicator.SetActive(true);
                    player.ui.entityIndicatorText.text = enemy.name;
                }else if (pickup != null)
                {
                    if (!player.ui.entityIndicator.activeSelf) player.ui.entityIndicator.SetActive(true);
                    player.ui.entityIndicatorText.text = pickup.pickupData.pickupName;
                    if(detection.distance <= 3)
                    {
                        player.ui.interactionIndicator.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(player.ui.interactionIndicator.GetComponent<CanvasGroup>().alpha, 1, Time.deltaTime * 10);
                    }
                    else
                    {
                        player.ui.interactionIndicator.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(player.ui.interactionIndicator.GetComponent<CanvasGroup>().alpha, 0, Time.deltaTime * 20);
                    }
                }
                else
                {
                    if (player.ui.entityIndicator.activeSelf) player.ui.entityIndicator.SetActive(false);
                    player.ui.interactionIndicator.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(player.ui.interactionIndicator.GetComponent<CanvasGroup>().alpha, 0, Time.deltaTime * 20);
                }
            }
            mouseLook.mouseSensitivityValve = player.stats.mouseSensitivity;
        }

        if (player.stats.isCrouching)
        {
            player.capsuleCollider.height = capsuleColliderCrouchHeight;
            //transform.localScale = new Vector3(transform.localScale.x, player.stats.crouchYScale, transform.localScale.x);
        }
        else
        {
            player.capsuleCollider.height = capsuleColliderInitHeight;
            //transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.x);
        }
        if (Input.GetKeyDown("k")) player.TakeDamageFromPlayer(100f, false);
    }
}
