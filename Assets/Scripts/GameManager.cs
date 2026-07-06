using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    private ObjectPoolManager objectPoolManager;

    [SerializeField]
    private ChickenSpawnManager chickenSpawnManager;

    [SerializeField]
    private ChickenDeliveryManager chickenDeliveryManager;

    [SerializeField]
    private DeliveryManManager deliveryManManager;

    [SerializeField]
    private PurchaseTriggerManager purchaseTriggerManager;

    [SerializeField]
    private MoneyManager moneyManager;

    [SerializeField]
    private UIManager uiManager;

    public CharacterManager CharacterManager { get { return characterManager; } }
    public ObjectPoolManager ObjectPoolManager { get { return objectPoolManager; } }
    public ChickenSpawnManager ChickenSpawnManager { get { return chickenSpawnManager; } }
    public ChickenDeliveryManager ChickenDeliveryManager { get { return chickenDeliveryManager; } }
    public DeliveryManManager DeliveryManManager { get { return deliveryManManager; } }
    public PurchaseTriggerManager PurchaseTriggerManager { get { return purchaseTriggerManager; } }
    public MoneyManager MoneyManager { get { return moneyManager; } }
    public UIManager UIManager { get { return uiManager; } }

    public GameConfig Config { get { return config; } }
    private GameConfig config;

    public DataAsset DataAsset { get { return dataAsset; } }
    private DataAsset dataAsset;

    public EffectConfig EffectConfig { get { return Config.Effect; } }

    private void Awake()
    {
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
        characterManager.Init();
        chickenSpawnManager.Init();
        deliveryManManager.Init();
        purchaseTriggerManager.Init();
    }

    private void LateUpdate()
    {
        characterManager.OnUpdate();
        chickenSpawnManager.OnUpdate();
        deliveryManManager.OnUpdate();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
