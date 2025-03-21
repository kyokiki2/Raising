using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDeliveryTrigger : BaseTrigger
{
    [SerializeField]
    private Transform foodParent;

    [SerializeField]
    private Grid grid;

    private float elapsedTime = 0f;
    private const float DURATION = 0.2f;

    public int Count { get { return chickenList.Count; } }
    private List<Chicken> chickenList = new();

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

        var playerMgr = GameManager.Instance.PlayerManager;
        if (playerMgr.ChickenCount <= 0)
            return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= DURATION)
        {
            var chicken = playerMgr.DropChicken();
            if (chicken != null)
            {
                chickenList.Add(chicken);
                SortChicken();
            }

            if (playerMgr.ChickenCount <= 0)
                playerMgr.Player.Animation.Carry(false);

            elapsedTime = 0f;
        }
    }

    private void SortChicken()
    {
        int columnMax = grid.Column;

        for (int i = 0; i < chickenList.Count; ++i)
        {
            var chicken = chickenList[i];
            if (chicken == null)
                continue;

            chicken.transform.SetParent(foodParent);

            float posX = (i % columnMax) * grid.PosX;
            int colCount = i / columnMax;
            float posY = colCount * grid.PosY;
            Vector3 targetPos = new Vector3(posX, posY, 0f);
            chicken.transform.localPosition = targetPos;
        }
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
