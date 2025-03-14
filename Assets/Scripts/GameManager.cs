using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager PlayerManager;
    public ResourcePoolManager ResourcePoolManager;
    public ChickenSpawnManager ChickenSpawnManager;

    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        PlayerManager.Init();
        ResourcePoolManager.Init();
        ChickenSpawnManager.Init();
    }


    private void LateUpdate()
    {
        PlayerManager.OnUpdate();
        ChickenSpawnManager.OnUpdate();
    }


    private void OnDestroy()
    {
        Instance = null;
    }
}
