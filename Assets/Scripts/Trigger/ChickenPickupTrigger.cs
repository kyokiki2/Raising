using UnityEngine;

public class ChickenPickupTrigger : BaseTrigger
{
    private ChickenSpawner spawner = null;

    private const int MAX_COUNT = 7;
    private float elapsedTime = 0f;
    private const float DURATION = 0.2f;

    protected override void OnEnter()
    {
        base.OnEnter();

        if(spawner == null)
            spawner = GetComponent<ChickenSpawner>();
    }


    protected override void OnUpdate()
    {
        base.OnUpdate();

        var playerMgr = GameManager.Instance.PlayerManager;
        if (playerMgr.ChickenCount >= MAX_COUNT)
            return;

        elapsedTime += Time.deltaTime;

        if(elapsedTime >= DURATION)
        {
            var chicken = spawner.GetChicken();
            if (chicken != null)
                playerMgr.SetChick(chicken);

            elapsedTime = 0f;
        }
    }
}
