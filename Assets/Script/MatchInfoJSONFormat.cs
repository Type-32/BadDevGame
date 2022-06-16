using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MatchInfoJSONFormat
{
    public struct MatchInformationType
    {
        public int botKillCount;
        public int lurkerKillCount;
        public int thrasherKillCount;
        public int damageDealt;
        public int kills;
        public int xpGained;
    };
    public List<MatchInformationType> matchInfoHistory = new List<MatchInformationType>();
}
