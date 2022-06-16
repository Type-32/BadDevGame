using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyButtonReferences : MonoBehaviour
{
    public UIManager ui;
    public FileManager file;
    public PlayerManager player;
    public Slider mouseSensitivitySettings;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        ui = FindObjectOfType<UIManager>();
        file = FindObjectOfType<FileManager>();
    }
    public void ApplySettings()
    {
        player.stats.mouseSensitivity = mouseSensitivitySettings.value;
        file.JSONplayerdata.mouseSensitivity = mouseSensitivitySettings.value;
        file.ApplyPlayerDataToFile();
    }
}
