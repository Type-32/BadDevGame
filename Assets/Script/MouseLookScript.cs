using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookScript : MonoBehaviour
{
    public PlayerManager player;
    private Vector2 MouseInput;
    private float mouseX;
    private float mouseY;
    public float mouseSensitivityValve;

    void Start()
    {
        transform.GetComponent<Camera>().fieldOfView = player.stats.cameraFieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivityValve = player.stats.mouseSensitivity;
        mouseX = 0f;
        mouseY = 0f;
    }

    void Update()
    {
        if(player.stats.mouseMovementEnabled){
            mouseX += Input.GetAxis("Mouse X") * mouseSensitivityValve * Time.deltaTime;
            mouseY += Input.GetAxis("Mouse Y") * mouseSensitivityValve * Time.deltaTime;
        }
        CameraRotationClamp(mouseY, player.stats.ClampCamRotX, player.stats.ClampCamRotZ);
        MouseInput = new Vector2(mouseX, -mouseY);
        if(player.stats.mouseMovementEnabled){
            if(!player.stats.invertedMouse){
                player.transform.rotation = Quaternion.Euler(0f, MouseInput.x, 0f);
                player.fpsCam.transform.localRotation = Quaternion.Euler(MouseInput.y, 0f, 0f);
            }else{
                player.transform.rotation = Quaternion.Euler(0f, -MouseInput.x, 0f);
                player.fpsCam.transform.localRotation = Quaternion.Euler(-MouseInput.y, 0f, 0f);
            }
        }
    }
    
    private void CameraRotationClamp(float y, float x, float z){
        mouseY = Mathf.Clamp(y, x, z);
    }
}
