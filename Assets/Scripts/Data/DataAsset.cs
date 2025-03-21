using System.Collections.Generic;
using UnityEngine;

public class DataAsset : ScriptableObject
{
    public SpawnData[] SpawnDatas;

    private Dictionary<int, SpawnData> spawnDataDic = new Dictionary<int, SpawnData>();
    public static DataAsset Load()
    {
        var dataAsset = Resources.Load("Data/DataAsset") as DataAsset;
        return dataAsset;
    }

    public void Init()
    {
        for(int i = 0; i < SpawnDatas.Length; ++i)
            spawnDataDic.Add(SpawnDatas[i].Id, SpawnDatas[i]);
    }

    public SpawnData GetSpawnData(int id)
    {
        SpawnData value;

        if (spawnDataDic.TryGetValue(id, out value))
            return value;

        return null;
    }
}
