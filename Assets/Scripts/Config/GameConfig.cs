using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameConfig : ScriptableObject
{
    public SettingDataConfig Data;
    public ResourceConfig Resource;
    public EffectConfig Effect;
    public static GameConfig Load()
    {
        var config = Resources.Load("GameConfig") as GameConfig;
        return config;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameConfig))]
public class GameConfigEditor : Editor
{
    private GameConfig config;
    private bool isStartData = false;
    private bool isResource = false;
    private bool isEffectData = false;

    public override void OnInspectorGUI()
    {
        if (IsOpenFolder(ref isStartData, "StartData"))
            DrawStartData();

        if (IsOpenFolder(ref isResource, "Resource"))
            DrawResource();

        if (IsOpenFolder(ref isEffectData, "Effect"))
            DrawEffect();


        EditorUtility.SetDirty(config);
    }

    private bool IsOpenFolder(ref bool isFolder, string folderName)
    {
        var isOpen = EditorGUILayout.Foldout(isFolder, folderName);
        if (isFolder != isOpen)
        {
            isFolder = isOpen;
            PlayerPrefs.SetInt(string.Format("{0}_{1}", typeof(GameConfig).Name, folderName), isFolder ? 1 : 0);
        }

        return isFolder;
    }

    private void DrawStartData()
    {
        EditorGUI.indentLevel++;

        config.Data.SpawnId = EditorGUILayout.IntField("SpawnId", config.Data.SpawnId);
        config.Data.CharPrice = EditorGUILayout.IntField("CharPrice", config.Data.CharPrice);

        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.LabelField("Character - Player");
        config.Data.Player.ChickenMax = EditorGUILayout.IntField("Chicken Max", config.Data.Player.ChickenMax);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.LabelField("Character - AI");
        config.Data.CharAI.ChickenMax = EditorGUILayout.IntField("Chicken Max", config.Data.CharAI.ChickenMax);
        EditorGUILayout.EndVertical();

        EditorGUI.indentLevel--;
    }

    private void DrawResource()
    {
        EditorGUI.indentLevel++;

        config.Resource.ChickenSpawner = EditorGUILayout.TextField("ChickenSpanwer", config.Resource.ChickenSpawner);
        config.Resource.DeliveryMan = EditorGUILayout.TextField("DeliveryMan", config.Resource.DeliveryMan);
        config.Resource.Chicken = EditorGUILayout.TextField("Chicken", config.Resource.Chicken);
        config.Resource.Money = EditorGUILayout.TextField("Money", config.Resource.Money);
        config.Resource.CharacterAI = EditorGUILayout.TextField("CharacterAI", config.Resource.CharacterAI);

        EditorGUI.indentLevel--;
    }

    private void DrawEffect()
    {
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.LabelField("Char - PickUp");
        config.Effect.CharPickUp.Power = EditorGUILayout.FloatField("Power", config.Effect.CharPickUp.Power);
        config.Effect.CharPickUp.Duration = EditorGUILayout.FloatField("Duration", config.Effect.CharPickUp.Duration);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.LabelField("Delivery - PickUp");
        config.Effect.DeliveryPickUp.Power = EditorGUILayout.FloatField("Power", config.Effect.DeliveryPickUp.Power);
        config.Effect.DeliveryPickUp.Duration = EditorGUILayout.FloatField("Duration", config.Effect.DeliveryPickUp.Duration);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.LabelField("DeliveryMan - PickUp");
        config.Effect.DeliveryManPickUp.Power = EditorGUILayout.FloatField("Power", config.Effect.DeliveryManPickUp.Power);
        config.Effect.DeliveryManPickUp.Duration = EditorGUILayout.FloatField("Duration", config.Effect.DeliveryManPickUp.Duration);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.LabelField("Money - Pay");
        config.Effect.MoneyPay.Power = EditorGUILayout.FloatField("Power", config.Effect.MoneyPay.Power);
        config.Effect.MoneyPay.Duration = EditorGUILayout.FloatField("Duration", config.Effect.MoneyPay.Duration);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.LabelField("Money - Get");
        config.Effect.MoneyGet.Power = EditorGUILayout.FloatField("Power", config.Effect.MoneyGet.Power);
        config.Effect.MoneyGet.Duration = EditorGUILayout.FloatField("Duration", config.Effect.MoneyGet.Duration);
        EditorGUILayout.EndVertical();


        config.Effect.SpawnTime = EditorGUILayout.FloatField("SpawnTime", config.Effect.SpawnTime);
        config.Effect.SpawnPickUp = EditorGUILayout.FloatField("SpawnPickUp", config.Effect.SpawnPickUp);
        config.Effect.TakeTime = EditorGUILayout.FloatField("DeliveryZone Take", config.Effect.TakeTime);
        config.Effect.MoneyHeight = EditorGUILayout.FloatField("MoneyHeight", config.Effect.MoneyHeight);

        EditorGUI.indentLevel--;
    }



    private void OnEnable()
    {
        if (config == null)
            config = (GameConfig)target;
    }

}
#endif

