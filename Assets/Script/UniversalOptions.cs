using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalOptions : MonoBehaviour
{
    public FileManager file;
    public PlayerManager player;
    public float mouseSensitivity = 70f;
    public float fieldOfView = 80f;
    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        file = FindObjectOfType<FileManager>();
        mouseSensitivity = file.JSONplayerdata.mouseSensitivity;
        fieldOfView = file.JSONplayerdata.fieldOfView;
    }
    public void ApplyOptions()
    {
        file.JSONplayerdata.mouseSensitivity = mouseSensitivity;
        file.JSONplayerdata.fieldOfView = fieldOfView;
        player.stats.mouseSensitivity = mouseSensitivity;
        player.fpsCam.GetComponent<Camera>().fieldOfView = fieldOfView;
        file.ApplyPlayerDataToFile();
        player.controls.aimingMouseSensitivity = player.stats.mouseSensitivity * 0.85f;
    }
}
