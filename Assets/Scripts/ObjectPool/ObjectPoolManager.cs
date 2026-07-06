using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    private readonly Dictionary<string, IPool> poolDic = new();

    private string GetKey<T>(string resourcePath) where T : Component
    {
        return $"{typeof(T).FullName}_{Path.GetFileName(resourcePath)}";
    }

    private ObjectPooling<T> GetPooling<T>(string resourcePath) where T : Component
    {
        var key = GetKey<T>(resourcePath);

        if (poolDic.TryGetValue(key, out var pool))
        {
            if (pool.ResourcePath != resourcePath &&
                pool.FileName != resourcePath)
            {
                Debug.LogError($"[ObjectPool] 파일명 충돌: {key} / 기존: {pool.ResourcePath}, 요청: {resourcePath}");
                return null;
            }

            return pool as ObjectPooling<T>;
        }
        return null;
    }

    private ObjectPooling<T> AddPooling<T>(string resourcePath) where T : Component
    {
        var key = GetKey<T>(resourcePath);

        var pool = new ObjectPooling<T>(resourcePath, parent);
        poolDic.Add(key, pool);

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
        {
            Debug.LogWarning($"[ObjectPool] 풀을 못 찾음: {GetKey<T>(obj.name)}");
            return;
        }

        find.Release(obj);
    }
    public void Release(GameObject obj)
    {
        Release(obj.transform);
    }
}
