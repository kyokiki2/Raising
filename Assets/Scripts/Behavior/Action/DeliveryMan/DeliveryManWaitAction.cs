using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DeliveryManWait", story: "DeliveryMan Waiting Check", category: "Action", id: "acbea4fd0dae9a79e2e9d144d8de2e87")]
public partial class DeliveryManWaitAction : Action
{

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var deliveryStationMgr = GameManager.Instance.ChickenDeliveryManager;
        if (deliveryStationMgr.IsHaveChicken == false)
            return Status.Running;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

