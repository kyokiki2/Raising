using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DeliveryManPickUp", story: "[DeliveryMan] pick up the chicken", category: "Action", id: "76369b1630438e055bbda0206ad3d9d9")]
public partial class DeliveryManPickUpAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> DeliveryMan;
    private Vector3 target;

    private DeliveryMan deliveryMan = null;
    private Chicken deliveryChicken = null;

    protected override Status OnStart()
    {
        target = GameManager.Instance.DeliveryManManager.GetWayPoint(DELIVERY_STATE.PICK_UP).position;
        deliveryMan = DeliveryMan.Value.GetComponent<DeliveryMan>();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Vector3 myPos = DeliveryMan.Value.transform.position;

        if (Vector3.Distance(myPos, target) <= 0.3f)
        {
            if(deliveryChicken == null)
                deliveryChicken = GameManager.Instance.ChickenDeliveryManager.GetChicken();

            if (deliveryChicken != null)
            {
                if(deliveryMan.Chicken == null)
                    deliveryMan.SetChicken(deliveryChicken);

                if (deliveryMan.IsPickUpDone)
                    return Status.Success;

            }
        }

        if (deliveryMan.IsIdleState)
            deliveryMan.SetDestination(target);

        return Status.Running;
    }

    protected override void OnEnd()
    {
        deliveryChicken = null;
        deliveryMan.IsPickUpDone = false;
    }
}

