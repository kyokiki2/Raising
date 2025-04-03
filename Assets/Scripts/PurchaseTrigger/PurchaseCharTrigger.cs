using UnityEngine;

public class PurchaseCharTrigger : PurchaseTrigger
{
    public override void Init()
    {
        price = GameManager.Instance.Config.Data.CharPrice;
        SetTitle();

        base.Init();
    }

    private void SetTitle()
    {
        titleText.text = "°í¿ë";
    }

    protected override void OnSuccess()
    {
        var character = GameManager.Instance.CharacterManager.CreateAIChar();
        character.transform.position = transform.position;

        base.OnSuccess();
    }
}
