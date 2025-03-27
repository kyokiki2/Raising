using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CharGetMoney", story: "[Character] go to get money", category: "Action", id: "ef7a90e5d9a8cb26faaf41e81a3885a1")]
public partial class CharGetMoneyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Character;
    private CharacterAI character = null;
    private Vector3 target = Vector3.zero;

    protected override Status OnStart()
    {
        character = Character.Value.GetComponent<CharacterAI>();

        if (GameManager.Instance.MoneyManager.IsEmpty)
            return Status.Success;
        else
        {
            target = GameManager.Instance.MoneyManager.GetMoneyRanPos();
            character.SetDestination(target);
            return Status.Running;
        }
    }

    protected override Status OnUpdate()
    {
        if (character.IsIdleState)
            character.SetDestination(target);

        if (character.IsMoveEnd)
            return Status.Success;

        return Status.Running;
    }

    protected override void OnEnd()
    {
        character = null;
        target = Vector3.zero;
    }
}

