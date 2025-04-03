using UnityEngine;
using DG.Tweening;
using System;

public class ChickenSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] parents;

    [SerializeField]
    private Transform charTransform;

    private Chicken[] chickenSpawns = null;
    private float elapsedTime = 0f;

    private SpawnData data = null;

    public Vector3 CharTargetPos { get { return charTransform.position; } }

    private ObjectPoolManager ObjectPoolManager
    {
        get { return GameManager.Instance.ObjectPoolManager; }
    }

    public void Init(SpawnData data)
    {
        this.data = data;

        if (chickenSpawns == null)
            chickenSpawns = new Chicken[parents.Length];
    }

    public void OnUpdate()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > GameManager.Instance.EffectConfig.SpawnTime)
        {
            var emptyIndex = GetEmptyIndex();

            if (0 <= emptyIndex)
                CreateNewChicken(emptyIndex);

            elapsedTime = 0f;
        }
    }

    private int GetEmptyIndex()
    {
        for (int i = 0; i < chickenSpawns.Length; ++i)
        {
            if (chickenSpawns[i] == null)
                return i;
        }

        return -1;
    }

    private void CreateNewChicken(int index)
    {
        var newChicken = ObjectPoolManager.ChickenPool.Get();
        chickenSpawns[index] = newChicken;
        chickenSpawns[index].transform.InitTransform(parents[index]);
        chickenSpawns[index].IsActive = true;
        CreateEffect(chickenSpawns[index]);
    }

    private void CreateEffect(Chicken chicken)
    {
        chicken.transform.localScale = Vector3.zero;
        chicken.transform.DOScale(1, 1f).SetEase(Ease.OutBack);
    }

    private void RemoveChicken(int index)
    {
        if (index < 0 || index >= chickenSpawns.Length)
            return;

        chickenSpawns[index] = null;
    }

    public Chicken GetChicken()
    {
        int lastIndex = Array.FindLastIndex(chickenSpawns, x => x != null);
        if (lastIndex < 0 || lastIndex >= chickenSpawns.Length)
            return null;

        var chicken = chickenSpawns[lastIndex];
        RemoveChicken(lastIndex);
        return chicken;
    }
}
