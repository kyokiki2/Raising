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

    public void SetMoneyValue(int money)
    {
        curMoney += money;
    }

    public void RemoveMoney(MoneyTrigger money)
    {
        GameManager.Instance.ObjectPoolManager.MoneyPool.Release(money);
        moneyList.Remove(money);
    }

    public void Buy(int price)
    {
        var prevMoney = curMoney;
        curMoney -= price;

        GameManager.Instance.UIManager.SetMoneyText(prevMoney, curMoney);
    }
}
