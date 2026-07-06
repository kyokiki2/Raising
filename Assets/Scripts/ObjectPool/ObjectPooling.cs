using System;
using System.IO;
using UnityEngine;
using UnityEngine.Pool;

public interface IPool
{
    string ResourcePath { get; }
    string FileName { get; }
    Type Type { get; }
}

public class ObjectPooling<T> : IPool where T : Component
{
    public string ResourcePath { get { return resourcePath; } }
    public string FileName { get { return Path.GetFileName(resourcePath); } }
    public Type Type { get { return typeof(T); } }

    private IObjectPool<T> pool = null;
    private string resourcePath = string.Empty;
    private Transform parent = null;

    public ObjectPooling(string resourcePath, Transform parent = null)
    {
        this.resourcePath = resourcePath;
        this.parent = parent;

        pool = new ObjectPool<T>(Create, null, (x) => x.gameObject.SetActive(false), GameObject.Destroy);
    }

    public void PreLoad(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            var compObject = Create();
            Release(compObject);
        }
    }

    public T Get()
    {
        var compObject = pool.Get();
        compObject.gameObject.SetActive(true);
        return compObject;
    }

    public void Release(T compObject)
    {
        compObject.transform.SetParent(parent);
        pool.Release(compObject);
    }

    private T Create()
    {
        var prefab = Resources.Load<GameObject>(resourcePath);
        var go = GameObject.Instantiate(prefab);
        go.name = FileName;
        go.transform.InitTransform(parent);
        return go.GetComponent<T>();
    }
}
