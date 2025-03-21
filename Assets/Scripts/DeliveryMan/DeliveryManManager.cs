using UnityEngine;
using System.Collections.Generic;

public class DeliveryManManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;

    private List<DeliveryMan> deliveryManList = new();

    public void Init()
    {
        AddDeliveryMan();
        //AddDeliveryMan();
        //AddDeliveryMan();
    }

    private void AddDeliveryMan()
    {
        var parent = wayPoints[(int)DELIVERY_STATE.WAIT];
        var deliveryMan = GameManager.Instance.ObjectPoolManager.DeliveryManPool.GetObject();
        deliveryMan.Init();
        deliveryMan.transform.InitTransform(parent);
        deliveryMan.gameObject.SetActive(true);
        deliveryManList.Add(deliveryMan);
    }

    public Transform GetWayPoint(DELIVERY_STATE type)
    {
        return wayPoints[(int)type];
    }
}
