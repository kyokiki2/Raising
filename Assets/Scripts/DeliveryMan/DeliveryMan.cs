using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryMan : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private BehaviorGraphAgent behavior;

    [SerializeField]
    private Transform foodParent;

    private DeliveryManManager manager { get { return GameManager.Instance.DeliveryManManager; } }

    private Chicken chicken = null;

    public void Init()
    {
        behavior.SetVariableValue("PickUp", manager.GetWayPoint(DELIVERY_STATE.PICK_UP));
        behavior.SetVariableValue("Delivery", manager.GetWayPoint(DELIVERY_STATE.DELIVERY));
    }

    public void SetChicken(Chicken chicken)
    {
        this.chicken = chicken;
        chicken.transform.InitTransform(foodParent);
    }

    public void DeliveryComplete()
    {
        var objectPoolManager = GameManager.Instance.ObjectPoolManager;

        objectPoolManager.ChickenPool.ReleaseObject(chicken);
        objectPoolManager.DeliveryManPool.ReleaseObject(this);
    }
}
