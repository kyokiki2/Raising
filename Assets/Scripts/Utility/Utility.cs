using UnityEngine;

public class Utility
{
    public static T ResourcesLoad<T>(string path, Transform parent) where T : MonoBehaviour
    {
        var prefab = Resources.Load(path) as GameObject;
        var go = GameObject.Instantiate(prefab);
        go.transform.InitTransform(parent);
        return go.GetComponent<T>();
    }
}
