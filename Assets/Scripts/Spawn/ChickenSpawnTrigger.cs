using UnityEngine;

public class ChickenSpawnTrigger : BaseTrigger
{
    private ChickenSpawner spawner = null;

    private float elapsedTime = 0f;
    private int maxCount = 0;

    protected override void OnEnter(CharacterBase character)
    {
        base.OnEnter(character);

        if(spawner == null)
            spawner = GetComponent<ChickenSpawner>();

        if (character is Player)
            maxCount = GameManager.Instance.Config.Data.Player.ChickenMax;
        else if(character is CharacterAI)
            maxCount = GameManager.Instance.Config.Data.CharAI.ChickenMax;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= GameManager.Instance.EffectConfig.SpawnPickUp)
        {
            for (int i = 0; i < charList.Count; ++i)
            {
                var character = charList[i];
                if (character.ChickenCount >= maxCount)
                    continue;

                var chicken = spawner.GetChicken();
                if (chicken != null)
                    character.SetChick(chicken);
            }

            elapsedTime = 0f;
        }
    }


}
