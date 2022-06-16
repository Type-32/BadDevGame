using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public UIManager ui = FindObjectOfType<UIManager>();
    public PlayerManager player = FindObjectOfType<PlayerManager>();
    // Start is called before the first frame update
    // Update is called once per frame
    public void ApplySettings(float sensitivity)
    {
        player.stats.mouseSensitivity = sensitivity;
    }
}
