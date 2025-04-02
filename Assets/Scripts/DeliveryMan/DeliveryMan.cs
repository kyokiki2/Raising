using System;
using DG.Tweening;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryMan : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent navMesh;

    [SerializeField]
    private BehaviorGraphAgent behavior;

    [SerializeField]
    private Transform foodParent;

    private DeliveryManManager manager { get { return GameManager.Instance.DeliveryManManager; } }

    public Chicken Chicken { get { return chicken; } }
    private Chicken chicken = null;
    private System.Action<DeliveryMan> onComplete = null;

    public bool IsPickUpDone { get { return isPickUpDone; } set { isPickUpDone = value; } }
    private bool isPickUpDone = false;

    public bool IsIdleState { get { return navMesh.IsIdleState(); } }

    public void Init()
    {
        if(onComplete == null)
            onComplete += manager.OnDeliveryComplete;

        behavior.SetVariableValue("PickUp", manager.GetWayPoint(DELIVERY_STATE.PICK_UP));
        behavior.SetVariableValue("Delivery", manager.GetWayPoint(DELIVERY_STATE.DELIVERY));
    }

    public void SetChicken(Chicken chicken)
    {
        this.chicken = chicken;
        chicken.transform.SetParent(foodParent);
        isPickUpDone = false;
        var config = GameManager.Instance.Config;
        chicken.transform.DOLocalJump(Vector3.zero, config.Effect.DeliveryManPickUp.Power, 1, config.Effect.DeliveryManPickUp.Duration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            isPickUpDone = true;
        });
    }

    public void DeliveryComplete()
    {
        gameObject.SetActive(false);

        if (onComplete != null)
            onComplete.Invoke(this);
    }

    public void SetDestination(Vector3 target)
    {
        navMesh.SetDestination(target);
    }

    public void SetState(DELIVERY_STATE state)
    {
        behavior.SetVariableValue("CurrentState", state);
    }

    public DELIVERY_STATE GetState()
    {
        BlackboardVariable<DELIVERY_STATE> state;
        behavior.GetVariable("CurrentState", out state);
        return state.Value;
    }

    public void Clear()
    {
        chicken = null;
    }

    private void OnDestroy()
    {
        onComplete = null;
    }
}
