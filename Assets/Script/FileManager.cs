using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    public JSONFormat JSONplayerdata;
    public string playerDataPath;
    public string matchInfoPath;
    // Start is called before the first frame update
    void Start()
    {
        playerDataPath = Application.persistentDataPath + "/playerdata.json";
        matchInfoPath = Application.persistentDataPath + "/matchinfohistory.json";
        JSONplayerdata = new JSONFormat();//Creating C# in pure json way
        //JSONplayerdata.playerName = "";
        if (!File.Exists(playerDataPath))
        {
            string jsonContents = JsonUtility.ToJson(JSONplayerdata, true);
            File.WriteAllText(playerDataPath, jsonContents);
        }
        else
        {
            string playerDataContent = File.ReadAllText(playerDataPath);
            JSONplayerdata = JsonUtility.FromJson<JSONFormat>(playerDataContent);
        }
    }
    public void ApplyPlayerDataToFile()
    {
        string jsonContents = JsonUtility.ToJson(JSONplayerdata, true);
        File.WriteAllText(playerDataPath, jsonContents);
    }
    public void ApplyMatchDataToFile(string writepath, MatchInfoJSONFormat writedata)
    {
        //JSONFormat writedata = new JSONFormat();
        string jsonContents = JsonUtility.ToJson(writedata, true);
        File.WriteAllText(writepath, jsonContents);
    }
}
