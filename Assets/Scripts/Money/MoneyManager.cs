using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] parents;

    public bool IsEmpty { get { return moneyList.Count <= 0; } }
    private List<Money> moneyList = new(); 

    private const float UP = 0.15f;

    public long CurMoney { get { return curMoney; } }
    private long curMoney = 0;

    public void SetMoney(Money money)
    {
        moneyList.Add(money);
    }

    public Transform GetParent()
    {
        int index = moneyList.Count % parents.Length;
        return parents[index];
    }

    public Vector3 GetMoneyRanPos()
    {
        int ranIdx = Random.Range(0, parents.Length);
        return parents[ranIdx].transform.position;
    }

    public float GetHeight()
    {
        if (moneyList.Count <= 0)
            return 0f;

        float calc = (float)moneyList.Count / parents.Length;
        return UP * calc;
    }

    public void EarnMoney(Money money, Vector3 targetPos)
    {
        var prevMoney = curMoney;
        money.transform.DOJump(targetPos, 2f, 1, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            curMoney += Money.Value;
            RemoveMoney(money);
            GameManager.Instance.UIManager.SetMoneyText(prevMoney, curMoney);
        });

    }

    public void RemoveMoney(Money money)
    {
        GameManager.Instance.ObjectPoolManager.MoneyPool.Release(money);
        moneyList.Remove(money);
    }
}
