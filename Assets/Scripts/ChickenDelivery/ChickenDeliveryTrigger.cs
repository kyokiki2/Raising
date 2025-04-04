using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDeliveryTrigger : BaseTrigger
{
    [SerializeField]
    private Transform foodParent;

    [SerializeField]
    private Transform charTransform;

    [SerializeField]
    private Grid grid;

    private float elapsedTime = 0f;

    public int Count { get { return chickenList.Count; } }
    private List<Chicken> chickenList = new();

    public Vector3 CharTargetPos { get { return charTransform.position; } }

    [Serializable]
    public class Grid
    {
        public float PosX;
        public float PosY;
        public int Column;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= GameManager.Instance.EffectConfig.TakeTime)
        {
            for (int i = 0; i < charList.Count; ++i)
            {
                var character = charList[i];
                if (character.ChickenCount <= 0)
                    continue;

                var chicken = character.DropChicken();
                if (chicken != null)
                {
                    SetChicken(chicken);
                }

                if (character.ChickenCount <= 0)
                    character.Animation.Carry(false);
            }           

            elapsedTime = 0f;
        }
    }

    private void SetChicken(Chicken chicken)
    {
        chickenList.Add(chicken);
        chicken.transform.SetParent(foodParent);
        Vector3 targetPos = GetNewChickenPos();
        var config = GameManager.Instance.Config;
        chicken.transform.DOLocalJump(targetPos, config.Effect.DeliveryPickUp.Power, 1, config.Effect.DeliveryPickUp.Duration).SetEase(Ease.OutQuad);
    }

    private Vector3 GetNewChickenPos()
    {
        Vector3 pos = Vector3.zero;
        int columnMax = grid.Column;

        for (int i = 0; i < chickenList.Count; ++i)
        {
            var chicken = chickenList[i];
            if (chicken == null)
                continue;

            float posX = (i % columnMax) * grid.PosX;
            int colCount = i / columnMax;
            float posY = colCount * grid.PosY;
            pos = new Vector3(posX, posY, 0f);
        }

        return pos;
    }

    public Chicken GetChicken()
    {
        if (chickenList.Count <= 0)
            return null;

        int lastIndex = chickenList.Count - 1;
        var chicken = chickenList[lastIndex];
        chickenList.RemoveAt(lastIndex);
        return chicken;
    }


}
