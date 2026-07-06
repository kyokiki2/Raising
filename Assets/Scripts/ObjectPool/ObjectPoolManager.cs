using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    private Dictionary<string, IPool> poolDic = new();

    private ObjectPooling<T> GetPooling<T>(string key) where T : Component
    {
        if (poolDic.TryGetValue(key, out var pool))
        {
            return pool as ObjectPooling<T>;
        }
        return null;
    }

    private ObjectPooling<T> AddPooling<T>(string resourcePath) where T : Component
    {
        var pool = new ObjectPooling<T>(resourcePath, parent);
        poolDic[pool.ResourcePath] = pool;

        if (!poolDic.ContainsKey(pool.FileName))
        {
            poolDic[pool.FileName] = pool;
        }

        return pool;
    }

    public T Get<T>(string resourcePath) where T : Component
    {
        var pool = GetPooling<T>(resourcePath);
        if (pool == null)
            pool = AddPooling<T>(resourcePath);

        return pool.Get();
    }

    public GameObject GetGameObject(string resourcePath)
    {
        var transform = Get<Transform>(resourcePath);
        return transform.gameObject;
    }

    public void PreLoad<T>(string resourcePath, int count = 1) where T : Component
    {
        var pool = GetPooling<T>(resourcePath);
        if (pool == null)
            pool = AddPooling<T>(resourcePath);

        pool.PreLoad(count);
    }

    public void Release<T>(T obj) where T : Component
    {
        var find = GetPooling<T>(obj.name);
        if (find == null)
            return;

        find.Release(obj);
    }

    public void Release(GameObject obj)
    {
        Release(obj.transform);
    }
}
