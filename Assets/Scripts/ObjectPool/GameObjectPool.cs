using UnityEngine;
using UnityEngine.Pool;
public class GameObjectPool
{
    private IObjectPool<GameObject> pool = null;
    private string resourcePath = string.Empty;

    private Transform Parent { get { return GameManager.Instance.ObjectPoolManager.Parent; } }

    public GameObjectPool(string resourcePath)
    {
        this.resourcePath = resourcePath;
        pool = new ObjectPool<GameObject>(Create, null, (x) => x.gameObject.SetActive(false), GameObject.Destroy);
    }

    public GameObject Get()
    {
        var go = pool.Get();
        go.SetActive(true);
        return go;
    }

    public void Release(GameObject go)
    {
        go.transform.SetParent(Parent);
        pool.Release(go);
    }

    private GameObject Create()
    {
        var prefab = Resources.Load<GameObject>(resourcePath);
        var go = GameObject.Instantiate(prefab);
        go.transform.InitTransform(Parent);
        return go;
    }
}
