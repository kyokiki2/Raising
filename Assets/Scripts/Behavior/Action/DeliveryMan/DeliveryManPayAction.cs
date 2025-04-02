using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using DG.Tweening;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DeliveryManPay", story: "[Customer] pays for the chicken", category: "Action", id: "5646ad248e0bd77ada7a0e003c932a1e")]
public partial class DeliveryManPayAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Customer;
    private DeliveryMan deliveryMan = null;
    private bool isDone = false;

    protected override Status OnStart()
    {
        isDone = false;
        deliveryMan = Customer.Value.GetComponent<DeliveryMan>();
        var money = GameManager.Instance.ObjectPoolManager.MoneyPool.Get();
        money.gameObject.SetActive(true);
        money.transform.InitTransform(deliveryMan.transform);
        var parent = GameManager.Instance.MoneyManager.GetParent();
        money.transform.SetParent(parent);
        money.transform.localEulerAngles = Vector3.zero;
        var height = GameManager.Instance.MoneyManager.GetHeight();
        Vector3 target = new Vector3(0f, height, 0f);
        var config = GameManager.Instance.Config;
        money.transform.DOLocalJump(target, config.Effect.MoneyPickUp.Power, 1, config.Effect.MoneyPickUp.Duration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            GameManager.Instance.MoneyManager.SetMoney(money);
            isDone = true;
        });

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (isDone)
            return Status.Success;

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

