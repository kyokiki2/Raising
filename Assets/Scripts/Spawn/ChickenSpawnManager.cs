using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    private List<ChickenSpawner> spawnerList = new ();

    public void Init()
    {
        int spawnId = GameManager.Instance.Config.SettingData.StartSpawnId;
        var spawnData = GameManager.Instance.DataAsset.GetSpawnData(spawnId);
        CreateSpawner(spawnData);
    }

    public void OnUpdate()
    {
        for(int i = 0; i < spawnerList.Count; ++i)
        {
            var spawner = spawnerList[i];
            spawner.OnUpdate();
        }
    }

    public void CreateSpawner(SpawnData spawnData)
    {
        var path = GameManager.Instance.Config.Resource.ChickenSpawner;
        var spawner = Utility.ResourcesLoad<ChickenSpawner>(path, parent);
        spawner.Init(spawnData);
        spawner.transform.SetLocalPositionAndRotation(spawnData.Position, Quaternion.Euler(spawnData.Rotation));
        spawner.transform.localScale = spawnData.Scale;

        spawnerList.Add(spawner);     
    }

    public Vector3 GetRanSpawnerPos()
    {
        int ranIdx = UnityEngine.Random.Range(0, spawnerList.Count);
        return spawnerList[ranIdx].CharTargetPos;
    }
}
