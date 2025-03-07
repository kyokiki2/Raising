using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController PlayerController;

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
        PlayerController.Init();
    }


    private void LateUpdate()
    {
        PlayerController.OnUpdate();
    }


    private void OnDestroy()
    {
        Instance = null;
    }
}
