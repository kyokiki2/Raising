using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Config : ScriptableObject
{
    public SettingDataConfig SettingData;
    public ResourceConfig Resource;
    public static Config Load()
    {
        var config = Resources.Load("Config") as Config;
        return config;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Config))]
public class ConfigEditor : Editor
{
    private Config config;
    private bool isResource = false;
    private bool isStartData = false;

    public override void OnInspectorGUI()
    {
        if (IsOpenFolder(ref isStartData, "Start_SettingData"))
            DrawStartData();

        if (IsOpenFolder(ref isResource, "Resource"))
            DrawResource();


        EditorUtility.SetDirty(config);
    }

    private bool IsOpenFolder(ref bool isFolder, string folderName)
    {
        var isOpen = EditorGUILayout.Foldout(isFolder, folderName);
        if (isFolder != isOpen)
        {
            isFolder = isOpen;
            PlayerPrefs.SetInt(string.Format("{0}_{1}", typeof(Config).Name, folderName), isFolder ? 1 : 0);
        }

        return isFolder;
    }

    private void DrawStartData()
    {
        EditorGUI.indentLevel++;

        config.SettingData.StartSpawnId = EditorGUILayout.IntField("StartSpawnId", config.SettingData.StartSpawnId);

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


    private void OnEnable()
    {
        if (config == null)
            config = (Config)target;
    }

}
#endif

