using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private DynamicJoystick joyStick;

    private List<Chicken> myChickenList = new List<Chicken>();
    private const float INTERVAL = 0.25f;
    public int ChickenCount { get { return myChickenList.Count; } }

    public Player Player { get { return player; } }

    public void Init()
    {
        player.Init(joyStick);
    }

    public void OnUpdate()
    {
        player.OnUpdate();
    }

    public void SetChick(Chicken chicken)
    {
        myChickenList.Add(chicken);
        Player.SetCarry(chicken);
        SortChicken();
    }

    private void SortChicken()
    {
        for(int i = 0; i < myChickenList.Count; ++i) 
        {
            var chicken = myChickenList[i];
            if (chicken == null)
                continue;

            var posY = i * INTERVAL;
            chicken.transform.localPosition = new Vector3(0f, posY, 0f);
        }
    }

}
