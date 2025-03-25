using UnityEngine.UI;
using DG.Tweening;

public static class UGUIExtension
{
    public static void ScoreTween(this Text text, long from, long end, float duration)
    {
        DOTween.To(() => from, x => from = x, end, duration)
               .OnUpdate(() =>
               {
                   text.text = from.ToString("N0");
               })
               .OnComplete(() =>
               {
                   text.text = end.ToString("N0");
               });
    }
}
