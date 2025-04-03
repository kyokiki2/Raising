using UnityEngine;
using System;

[Serializable]
public class SettingDataConfig
{
    public int SpawnId;
    public int MoneyValue;
    public int CharPrice;
    public SpawnerPriceData SpawnerPrice;
    public CharData Player;
    public CharData CharAI;
}

[Serializable]
public class  CharData
{
    public int ChickenMax; 
}

[Serializable]
public class SpawnerPriceData
{
    public int Id;
    public int Price;
}
