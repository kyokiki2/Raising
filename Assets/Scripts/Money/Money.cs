using UnityEngine;
using DG.Tweening;

public class Money : BaseTrigger
{
    public const int Value = 50;

    protected override void OnEnter()
    {
        base.OnEnter();

        //var player = GameManager.Instance.PlayerManager.Player;
        //Vector3 target = player.transform.position;
        //transform.DOJump(target, 1f, 1, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
        //{
        //    GameManager.Instance.MoneyManager.RemoveMoney(this);
        //});

        GameManager.Instance.MoneyManager.EarnMoney(this);
    }


}
