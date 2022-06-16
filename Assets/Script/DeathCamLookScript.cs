using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamLookScript : MonoBehaviour
{
    private Vector2 MouseInput;
    private float mouseX;
    private float mouseY;
    private float mouseSensitivity;
    private float ClampCamRotX;
    private float ClampCamRotZ;
    private bool mouseMovementEnabled = true;
    private bool invertedMouse = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseX = 0f;
        mouseY = 0f;
    }

    void Update()
    {
        if (mouseMovementEnabled)
        {
            mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        }
        CameraRotationClamp(mouseY, ClampCamRotX, ClampCamRotZ);
        MouseInput = new Vector2(mouseX, -mouseY);
        if (mouseMovementEnabled)
        {
            if (!invertedMouse)
            {
                transform.rotation = Quaternion.Euler(MouseInput.y, MouseInput.x, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(-MouseInput.y, -MouseInput.x, 0f);
            }
        }
    }

    private void CameraRotationClamp(float y, float x, float z)
    {
        mouseY = Mathf.Clamp(y, x, z);
    }
    public void InitializeOnPlayerDeath(float sensitivity, float ClampRotationX, float ClampRotationZ, bool mouseMovementToggle, bool invertMouse)
    {
        mouseMovementEnabled = mouseMovementToggle;
        mouseSensitivity = sensitivity;
        ClampCamRotX = ClampRotationX;
        ClampCamRotZ = ClampRotationZ;
        invertedMouse = invertMouse;
    }
}
