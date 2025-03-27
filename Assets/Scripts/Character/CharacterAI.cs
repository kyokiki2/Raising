using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : CharacterBase
{
    [SerializeField]
    private NavMeshAgent navMesh;

    public bool IsIdleState { get { return navMesh.IsIdleState(); } }
    public bool IsMoveEnd { get { return navMesh.IsMoveEnd(); } }

    public override void Init()
    {
        base.Init();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        float speed = navMesh.velocity.magnitude;
        charAnimation.Move(speed);
    }

    public void SetDestination(Vector3 target)
    {
        navMesh.SetDestination(target);
    }
}
