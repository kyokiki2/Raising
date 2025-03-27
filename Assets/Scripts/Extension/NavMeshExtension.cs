using UnityEngine;
using UnityEngine.AI;

public static class NavMeshExtension
{
    public static bool IsIdleState(this NavMeshAgent naviMesh)
    {
        if (!naviMesh.pathPending && naviMesh.remainingDistance <= naviMesh.stoppingDistance)
        {
            return true;
        }

        return false;
    }

    public static bool IsMoveEnd(this NavMeshAgent naviMesh)
    {
        if (!naviMesh.pathPending && naviMesh.remainingDistance <= naviMesh.stoppingDistance)
        {
            if (!naviMesh.hasPath || naviMesh.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }
}
