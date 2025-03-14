using UnityEngine;
using UnityEngine.Pool;

public class ResourcePoolManager : MonoBehaviour
{
    [SerializeField]
    private Transform foodParent;

    private IObjectPool<Chicken> chickenPool = null;

    public void Init()
    {
        if (chickenPool == null)
            chickenPool = new ObjectPool<Chicken>(CreateChicken, null, (x) => x.gameObject.SetActive(false), (x) => GameObject.Destroy(x));
    }

    public Chicken GetChicken()
    {
        var chicken = chickenPool.Get();
        chicken.gameObject.SetActive(false);
        return chicken;
    }

    public void ReturnToChicken(Chicken chicken)
    {
        chicken.transform.SetParent(foodParent);
        chickenPool.Release(chicken);
    }

    private Chicken CreateChicken()
    {
        var prefab = Resources.Load<GameObject>("Food/Chicken");
        var go = GameObject.Instantiate(prefab);
        go.transform.InitTransform(foodParent);
        return go.GetComponent<Chicken>();
    }

}
