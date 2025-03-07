using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private DynamicJoystick joyStick;

    public void Init()
    {
        player.Init(joyStick);
    }

    public void OnUpdate()
    {
        player.OnUpdate();
    }
}
