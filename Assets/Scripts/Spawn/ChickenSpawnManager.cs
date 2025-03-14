using DG.Tweening;
using System;
using UnityEngine;

public class ChickenSpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] parents;

    private Chicken[] chickenSpawns = null;
    private float elapsedTime = 0f;
    private const float DURATION = 2f;

    private ResourcePoolManager resourcePoolManager
    {
        get { return GameManager.Instance.ResourcePoolManager; }
    }

    public void Init()
    {
        if (chickenSpawns == null)
            chickenSpawns = new Chicken[parents.Length];
    }

    public void OnUpdate()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > DURATION) 
        {
            var emptyIndex = GetEmptyIndex();

            if (0 <= emptyIndex)
                CreateNewChicken(emptyIndex);

            elapsedTime = 0f;
        }
    }

    private int GetEmptyIndex()
    {
        for(int i = 0; i < chickenSpawns.Length; ++i)
        {
            if (chickenSpawns[i] == null)
                return i;
        }

        return -1;
    }

    private void CreateNewChicken(int index)
    {
        var newChicken = resourcePoolManager.GetChicken();
        chickenSpawns[index] = newChicken;
        chickenSpawns[index].transform.InitTransform(parents[index]);
        chickenSpawns[index].IsActive = true;
        CreateEffect(chickenSpawns[index]);
    }

    private void CreateEffect(Chicken chicken)
    {
        chicken.transform.localScale = Vector3.zero;
        chicken.transform.DOScale(1, 2f).SetEase(Ease.OutBack);
    }

    private void RemoveChicken(int index)
    {
        if (index < 0 || index >= chickenSpawns.Length)
            return;

        chickenSpawns[index] = null;
    }

    public Chicken GetChicken()
    {
        for(int i = 0; i < chickenSpawns.Length; ++i)
        {
            var chicken = chickenSpawns[i];

            if (chicken == null)
                continue;

            RemoveChicken(i);

            return chicken;
        }

        return null;
    }


}
