using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text moneyText;

    public void SetMoneyText(long from, long end)
    {
        DOTween.To(() => from, x => from = x, end, 0.5f)
       .OnUpdate(() =>
       {
           moneyText.text = string.Format("{0}¿ø", from.ToString("N0"));
       })
       .OnComplete(() =>
       {
           moneyText.text = string.Format("{0}¿ø", end.ToString("N0"));
       });
    }
}
