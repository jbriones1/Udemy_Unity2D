using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

[System.Serializable]
public class Encounter
{
    public string encounterName;
    public int encounterID;
    public string encounterDescription;

    public Systems systemReq0, systemReq1, systemReq2;
    public Systems systemRew0,systemRew1;

    public PlayerResources resourceCost0, resourceCost1, resourceRew0, resourceRew1, resourceRew2; // Rew = reward

    public DamageType damageType0;

    // Encounter constructor
    public Encounter(string name, int id, string description, Systems req0, Systems req1, Systems req2)
    {
        encounterName = name;
        encounterID = id;

    }
}
