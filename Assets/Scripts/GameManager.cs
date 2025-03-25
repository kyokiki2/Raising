using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager CharacterManager;
    public ObjectPoolManager ObjectPoolManager;
    public ChickenSpawnManager ChickenSpawnManager;
    public DeliveryManManager DeliveryManManager;
    public DeliveryStationManager DeliveryStationManager;
    public MoneyManager MoneyManager;
    public UIManager UIManager;

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
        CharacterManager.Init();
        ChickenSpawnManager.Init();
        DeliveryManManager.Init();
    }


    private void LateUpdate()
    {
        CharacterManager.OnUpdate();
        ChickenSpawnManager.OnUpdate();
        DeliveryManManager.OnUpdate();
    }


    private void OnDestroy()
    {
        Instance = null;
    }
}
