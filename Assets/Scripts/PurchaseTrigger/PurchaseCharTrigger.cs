using UnityEngine;

public class PurchaseCharTrigger : ProgressBarTrigger
{
    protected override void OnEnter(CharacterBase character)
    {
        bool isPlayer = character is Player;
        if (isPlayer == false)
            return;

        if(GameManager.Instance.MoneyManager.CurMoney < GameManager.Instance.Config.Data.CharPrice)
            return;

        base.OnEnter(character);
    }

    protected override void OnSuccess()
    {
        var character = GameManager.Instance.CharacterManager.CreateAIChar();
        character.transform.position = transform.position;

        base.OnSuccess();
    }
}
