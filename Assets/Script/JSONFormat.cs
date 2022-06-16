using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JSONFormat
{
    public float mouseSensitivity = 70f;
    public string playerName = "";
    public float fieldOfView = 80f;
    public int xpLevel = 1;
    public int xp = 0;

    public int totalKillCount = 0;
    public int totalDamageDealt = 0;
    public int matchesAttended = 0;
    public int totalBotKillCount = 0;
    public int totalLurkerKillCount = 0;
    public int totalThrasherKillCount = 0;
}
