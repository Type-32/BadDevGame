using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCam : MonoBehaviour
{
    private Vector2 MouseInput;
    private float mouseX;
    private float mouseY;
    public float mouseSensitivity = 70f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseX = 0f;
        mouseY = 0f;
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //CameraRotationClamp(mouseY, -90, 90);
        MouseInput = new Vector2(mouseX, -mouseY);
        
        transform.rotation = Quaternion.Euler(0f, MouseInput.x, 0f);
        transform.localRotation = Quaternion.Euler(MouseInput.y, 0f, 0f);

        transform.rotation = Quaternion.Euler(0f, -MouseInput.x, 0f);
        transform.localRotation = Quaternion.Euler(-MouseInput.y, 0f, 0f);
    }

    private void CameraRotationClamp(float y, float x, float z)
    {
        mouseY = Mathf.Clamp(y, x, z);
    }
}
