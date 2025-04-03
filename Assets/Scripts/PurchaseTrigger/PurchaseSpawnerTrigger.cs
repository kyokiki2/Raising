using UnityEngine;

public class PurchaseSpawnerTrigger : PurchaseTrigger
{
    public override void Init()
    {
        price = GameManager.Instance.Config.Data.SpawnerPrice.Price;
        SetTitle();

        base.Init();
    }

    private void SetTitle()
    {
        titleText.text = "°Ç¹°";
    }

    protected override void OnSuccess()
    {
        int spawnerId = GameManager.Instance.Config.Data.SpawnerPrice.Id;
        var spawnData = GameManager.Instance.DataAsset.GetSpawnData(spawnerId);
        GameManager.Instance.ChickenSpawnManager.CreateSpawner(spawnData);

        base.OnSuccess();
    }
}
