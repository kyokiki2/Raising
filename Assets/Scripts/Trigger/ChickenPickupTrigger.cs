using UnityEngine;

public class ChickenPickupTrigger : BaseTrigger
{
    private const int MAX_COUNT = 7;
    private float elapsedTime = 0f;
    private const float DURATION = 1f;

    protected override void OnUpdate()
    {
        base.OnUpdate();

        var playerMgr = GameManager.Instance.PlayerManager;
        if (playerMgr.ChickenCount >= MAX_COUNT)
            return;

        elapsedTime += Time.deltaTime;

        if(elapsedTime >= DURATION)
        {
            var chicken = GameManager.Instance.ChickenSpawnManager.GetChicken();
            if (chicken != null)
                playerMgr.SetChick(chicken);

            elapsedTime = 0f;
        }
    }
}
