using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Values")]
    [Range(1f, 200f)] public float health = 100f;
    [Range(1f, 100f)] public float armor = 100f;
    [Range(1f, 200f)] public float healthLimit = 100f;
    [Range(1f, 100f)] public float armorLimit = 100f;
    public float speed = 4f;
    public float sprintSpeed = 8f;
    public float slideSpeed = 10f;
    public float crouchSpeed = 3.2f;
    public float slideDuration = 1.2f;
    [Range(0f, 10f)] public float jumpForce = 10f;
    [Range(0f, 1f)] public float armorResistance = 0.3f;
    public float crouchYScale;

    [Space]
    [Header("Player Options")]
    public bool toggleAiming = false;
    public bool invertedMouse = false;
    
    [Space]
    [Header("Player States")]
    public bool isSprinting = false;
    public bool isAiming = false;
    public bool isWalking = false;
    public bool isJumping = false;
    public bool isCrouching = false;
    public bool isSliding = false;
    public bool onGround = false;

    [Space]
    [Header("Player Generic Controls")]
    public bool mouseMovementEnabled = true;
    public bool playerMovementEnabled = true;
    public bool gunInteractionEnabled = true;
    public bool playerCameraBobEnabled = true;

    [Space]
    [Header("Player Camera Settings")]
    [Range(60f, 120f)] public float cameraFieldOfView = 60f;
    [Range(0f, 200f)] public float mouseSensitivity = 70f;
    [Range(-5f, -90f)] public float ClampCamRotX = -85f;
    [Range(5f, 90f)] public float ClampCamRotZ = 85f;
    [Range(1f, 1.5f)] public float sprintFOVMultiplier = 1.1f;
    [Range(10f, 20f)] public float sprintFOVChangeDuration = 12;

    private void Update()
    {
        if (armor < 0) armor = 0f;
        if (health < 0) health = 0f;
    }
}
