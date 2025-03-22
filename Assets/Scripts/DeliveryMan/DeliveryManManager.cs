using UnityEngine;
using System.Collections.Generic;

public class DeliveryManManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;

    private List<DeliveryMan> deliveryManList = new();

    private float elapsedTime = 0f;
    private const float DURATION = 1f;
    private const int MAX = 10;

    private int ChickenCount { get { return GameManager.Instance.DeliveryStationManager.ChickenCount; } }

    public void Init()
    {
        //AddDeliveryMan();
    }

    private void AddDeliveryMan()
    {
        var parent = wayPoints[(int)DELIVERY_STATE.WAIT];
        var deliveryMan = GameManager.Instance.ObjectPoolManager.DeliveryManPool.GetObject();
        deliveryMan.Init();
        deliveryMan.transform.InitTransform(parent);
        deliveryMan.gameObject.SetActive(true);
        deliveryMan.name = deliveryManList.Count.ToString();
        deliveryManList.Add(deliveryMan);
    }

    public void OnDeliveryComplete(DeliveryMan deliveryMan)
    {
        var objectPoolManager = GameManager.Instance.ObjectPoolManager;
        objectPoolManager.ChickenPool.ReleaseObject(deliveryMan.Chicken);
        objectPoolManager.DeliveryManPool.ReleaseObject(deliveryMan);
        deliveryMan.Clear();
        Remove(deliveryMan);
    }

    public void Remove(DeliveryMan deliveryMan)
    {
        deliveryManList.Remove(deliveryMan);
    }

    public Transform GetWayPoint(DELIVERY_STATE type)
    {
        return wayPoints[(int)type];
    }

    private bool IsReadyToDeliver()
    {
        bool isReady = deliveryManList.Count <= 0;

        if(deliveryManList.Count < MAX)
        {
            for (int i = 0; i < deliveryManList.Count; ++i)
            {
                var deliveryMan = deliveryManList[i];
                if (deliveryMan == null)
                    continue;

                if (deliveryMan.GetState() == DELIVERY_STATE.DELIVERY)
                    isReady = true;
            }
        }

        return isReady;
    }

    public void OnUpdate()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > DURATION)
        {
            if (deliveryManList.Count >= ChickenCount)
                return;

            if (IsReadyToDeliver())
                AddDeliveryMan();

            elapsedTime = 0f;
        }
    }


}
