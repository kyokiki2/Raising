using UnityEngine;
using DG.Tweening;

public class MoneyTrigger : BaseTrigger
{
    public const int Value = 50;

    protected override void OnEnter(CharacterBase character)
    {
        base.OnEnter(character);
        GameManager.Instance.MoneyManager.EarnMoney(this, character.FoodParent.position);
    }


}
