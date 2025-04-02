using UnityEngine;

public class ChickenSpawnTrigger : BaseTrigger
{
    private ChickenSpawner spawner = null;

    private const int MAX_COUNT = 7;
    private float elapsedTime = 0f;
    private const float DURATION = 0.2f;

    protected override void OnEnter(CharacterBase character)
    {
        base.OnEnter(character);

        if(spawner == null)
            spawner = GetComponent<ChickenSpawner>();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= DURATION)
        {
            for (int i = 0; i < charList.Count; ++i)
            {
                var character = charList[i];
                if (character.ChickenCount >= MAX_COUNT)
                    continue;

                var chicken = spawner.GetChicken();
                if (chicken != null)
                    character.SetChick(chicken);
            }

            elapsedTime = 0f;
        }
    }
}
