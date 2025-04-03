using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterManager CharacterManager;
    public ObjectPoolManager ObjectPoolManager;
    public ChickenSpawnManager ChickenSpawnManager;
    public ChickenDeliveryManager ChickenDeliveryManager;
    public DeliveryManManager DeliveryManManager;
    public PurchaseTriggerManager PurchaseTriggerManager;
    public MoneyManager MoneyManager;
    public UIManager UIManager;

    public static GameManager Instance = null;

    public GameConfig Config { get { return config; } }
    private GameConfig config;

    public DataAsset DataAsset { get { return dataAsset; } }
    private DataAsset dataAsset;

    public EffectConfig EffectConfig { get { return Config.Effect; } }

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
            config = GameConfig.Load();

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
        PurchaseTriggerManager.Init();
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
