using UnityEngine;
using System;

[Serializable]
public class SettingDataConfig
{
    public int SpawnId;
    public int CharPrice;
    public CharData Player;
    public CharData CharAI;
}

[Serializable]
public class  CharData
{
    public int ChickenMax; 
}
