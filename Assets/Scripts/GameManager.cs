using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager PlayerManager;
    public ObjectPoolManager ObjectPoolManager;
    public ChickenSpawnManager ChickenSpawnManager;
    public DeliveryManManager DeliveryManManager;
    public DeliveryStationManager DeliveryStationManager;

    public static GameManager Instance = null;

    public Config Config { get { return config; } }
    private Config config;

    public DataAsset DataAsset { get { return dataAsset; } }
    private DataAsset dataAsset;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Load();
        Init();
    }

    private void Load()
    {
        if (config == null)
            config = Config.Load();

        if (dataAsset == null)
            dataAsset = DataAsset.Load();
    }

    private void Init()
    {
        dataAsset.Init();
        ObjectPoolManager.Init();
        PlayerManager.Init();
        ChickenSpawnManager.Init();
        DeliveryManManager.Init();
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
