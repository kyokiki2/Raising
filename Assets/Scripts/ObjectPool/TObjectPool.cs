using UnityEngine;
using UnityEngine.Pool;

public class TObjectPool<T> where T : MonoBehaviour
{
    private IObjectPool<T> pool = null;
    private string resourcePath = string.Empty;

    private Transform Parent { get { return GameManager.Instance.ObjectPoolManager.Parent; } }

    public TObjectPool(string resourcePath)
    {
        this.resourcePath = resourcePath;
        pool = new ObjectPool<T>(Create, null, (x) => x.gameObject.SetActive(false), GameObject.Destroy);
    }

    public T Get()
    {
        var compObject = pool.Get();
        compObject.gameObject.SetActive(false);
        return compObject;
    }

    public void Release(T compObject)
    {
        compObject.transform.SetParent(Parent);
        pool.Release(compObject);
    }

    private T Create()
    {
        var prefab = Resources.Load<GameObject>(resourcePath);
        var go = GameObject.Instantiate(prefab);
        go.transform.InitTransform(Parent);
        return go.GetComponent<T>();
    }
}
