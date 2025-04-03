using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] parents;

    public bool IsEmpty { get { return moneyList.Count <= 0; } }
    private List<MoneyTrigger> moneyList = new(); 

    public long CurMoney { get { return curMoney; } }
    private long curMoney = 0;

    public void SetMoney(MoneyTrigger money)
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
        return GameManager.Instance.EffectConfig.MoneyHeight * calc;
    }

    public void EarnMoney(MoneyTrigger money, Vector3 targetPos)
    {
        var prevMoney = curMoney;
        var power = GameManager.Instance.EffectConfig.MoneyGet.Power;
        var duration = GameManager.Instance.EffectConfig.MoneyGet.Duration;
        money.transform.DOJump(targetPos, power, 1, duration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            curMoney += MoneyTrigger.Value;
            RemoveMoney(money);
            GameManager.Instance.UIManager.SetMoneyText(prevMoney, curMoney);
        });

    }

    public void RemoveMoney(MoneyTrigger money)
    {
        GameManager.Instance.ObjectPoolManager.MoneyPool.Release(money);
        moneyList.Remove(money);
    }
}
