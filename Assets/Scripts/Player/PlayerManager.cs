using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

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
        Vector3 targetPos = GetNewChickenPos();
        chicken.transform.DOLocalJump(targetPos, 0.8f, 1, 0.2f).SetEase(Ease.OutQuad);
    }

    public Chicken DropChicken()
    {
        if (myChickenList.Count <= 0)
            return null;

        int lastIndex = myChickenList.Count - 1;
        var chicken = myChickenList[lastIndex];
        myChickenList.RemoveAt(lastIndex);
        return chicken;
    }

    private Vector3 GetNewChickenPos()
    {
        Vector3 pos = Vector3.one;
        for (int i = 0; i < myChickenList.Count; ++i)
        {
            var chicken = myChickenList[i];
            if (chicken == null)
                continue;

            var posY = i * INTERVAL;
            pos = new Vector3(0f, posY, 0f);
        }

        return pos;
    }

}
