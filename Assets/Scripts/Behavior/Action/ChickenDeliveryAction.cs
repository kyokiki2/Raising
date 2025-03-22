using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChickenDelivery", story: "[DeliveryMan] delivers chicken", category: "Action", id: "6db200b80ee84cae6bed3a5c16580d06")]
public partial class ChickenDeliveryAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> DeliveryMan;

    private Vector3 target;
    private DeliveryMan deliveryMan = null;

    protected override Status OnStart()
    {
        target = GameManager.Instance.DeliveryManManager.GetWayPoint(DELIVERY_STATE.DELIVERY).position;
        deliveryMan = DeliveryMan.Value.GetComponent<DeliveryMan>();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Vector3 myPos = DeliveryMan.Value.transform.position;
        if (Vector3.Distance(myPos, target) <= 1f)
        {
            deliveryMan.DeliveryComplete();
            return Status.Success;
        }

        if (deliveryMan.IsIdleState())
            deliveryMan.SetDestination(target);

        return Status.Running;
    }

    protected override void OnEnd()
    {
        deliveryMan = null;
    }
}

