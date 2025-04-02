using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected CharAnimation charAnimation;

    [SerializeField]
    protected CharacterController controller;

    [SerializeField]
    protected Transform foodParent;

    public CharAnimation Animation { get { return charAnimation; } }
    public Transform FoodParent { get { return foodParent; } }

    private List<Chicken> myChickenList = new List<Chicken>();
    private const float INTERVAL = 0.25f;
    public int ChickenCount { get { return myChickenList.Count; } }

    public virtual void Init()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public void SetCarry(Chicken chicken)
    {
        if (charAnimation.IsCarry == false)
            charAnimation.Carry(true);

        chicken.transform.SetParent(foodParent);
    }

    public void SetChick(Chicken chicken)
    {
        myChickenList.Add(chicken);
        SetCarry(chicken);
        Vector3 targetPos = GetNewChickenPos();
        var config = GameManager.Instance.Config;
        chicken.transform.DOLocalJump(targetPos, config.Effect.CharPickUp.Power, 1, config.Effect.CharPickUp.Duration).SetEase(Ease.OutQuad);
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
