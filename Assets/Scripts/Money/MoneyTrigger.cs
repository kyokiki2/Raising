using UnityEngine;
using DG.Tweening;

public class MoneyTrigger : BaseTrigger
{
    [SerializeField]
    private BoxCollider boxCollider;

    private MoneyManager moneyManager { get { return GameManager.Instance.MoneyManager; } }

    protected override void OnEnter(CharacterBase character)
    {
        base.OnEnter(character);

        boxCollider.enabled = false;

        var prevMoney = moneyManager.CurMoney;
        var power = GameManager.Instance.EffectConfig.MoneyGet.Power;
        var duration = GameManager.Instance.EffectConfig.MoneyGet.Duration;
        Vector3 targetPos = character.FoodParent.position;
        transform.DOJump(targetPos, power, 1, duration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            moneyManager.SetMoneyValue(GameManager.Instance.Config.Data.MoneyValue);
            GameManager.Instance.UIManager.SetMoneyText(prevMoney, moneyManager.CurMoney);
            moneyManager.RemoveMoney(this);
            boxCollider.enabled = true;
        });
    }

}
