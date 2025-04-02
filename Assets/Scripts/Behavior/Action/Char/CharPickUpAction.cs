using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CharPickUp", story: "[Character] go to pickup chicken", category: "Action", id: "5ef3908933681153e467c58a649ba020")]
public partial class CharPickUpAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Character;
    private CharacterAI character = null;
    private Vector3 target = Vector3.zero;
    private const int MAX_COUNT = 5;

    protected override Status OnStart()
    {
        character = Character.Value.GetComponent<CharacterAI>();
        target = GameManager.Instance.ChickenSpawnManager.GetRanSpawnerPos();
        character.SetDestination(target);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(character.ChickenCount > MAX_COUNT)
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

