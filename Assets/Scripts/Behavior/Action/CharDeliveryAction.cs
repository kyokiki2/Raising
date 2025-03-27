using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CharDelivery", story: "[Character] go to delivery", category: "Action", id: "70a6ef77493da8eb320cd5cad80d1e8d")]
public partial class CharDeliveryAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Character;
    private CharacterAI character = null;
    private Vector3 target = Vector3.zero;

    protected override Status OnStart()
    {
        character = Character.Value.GetComponent<CharacterAI>();
        target = GameManager.Instance.DeliveryStationManager.CharTargetPos;
        character.SetDestination(target);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (character.ChickenCount <= 0)
            return Status.Success;

        if (character.IsIdleState)
            character.SetDestination(target);

        return Status.Running;
    }

    protected override void OnEnd()
    {
        target = Vector3.zero;
        character = null;
    }
}

