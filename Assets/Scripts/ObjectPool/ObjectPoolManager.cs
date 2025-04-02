using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    public Transform Parent { get { return parent; } }

    public TObjectPool<Chicken> ChickenPool { get; private set; }
    public TObjectPool<DeliveryMan> DeliveryManPool { get; private set; }
    public TObjectPool<MoneyTrigger> MoneyPool { get; private set; }

    public void Init()
    {
        var resourceInfo = GameManager.Instance.Config.Resource;
        ChickenPool = new TObjectPool<Chicken>(resourceInfo.Chicken);
        DeliveryManPool = new TObjectPool<DeliveryMan>(resourceInfo.DeliveryMan);
        MoneyPool = new TObjectPool<MoneyTrigger>(resourceInfo.Money);
    }
}
