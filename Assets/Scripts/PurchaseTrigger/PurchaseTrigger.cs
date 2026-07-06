using System.Collections;
using TMPro;
using UnityEngine;

public class PurchaseTrigger : BaseTrigger
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    protected TextMeshPro titleText;

    [SerializeField]
    private TextMeshPro priceText;

    private const float DURATION = 3f;
    private const string FILL_AMOUNT = "_FillAmount";

    private float elapsedTime = 0f;
    private bool isGaugeFull = false;
    protected int price = 0;

    private enum PROGRESS_BAR
    {
        BACK = 0,
        FRONT = 1
    }

    public virtual void Init()
    {
        priceText.text = string.Format("{0}원", price.ToString("N0"));
    }

    protected override bool IsEnter(CharacterBase character)
    {
        bool isPlayer = character is Player;
        if (isPlayer == false)
            return false;

        if (GameManager.Instance.MoneyManager.CurMoney < price)
            return false;

        return true;
    }

    protected override void OnEnter(CharacterBase character)
    {
        base.OnEnter(character);

        isGaugeFull = false;
        elapsedTime = 0f;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (isGaugeFull == false)
        {
            UpdateGauge();
            isGaugeFull = GetProgressBar() == 1f;
            if (isGaugeFull)
                OnSuccess();
        }
    }

    protected override void OnExit(CharacterBase character)
    {
        base.OnExit(character);

        SetProgressBar(0f);
    }

    protected virtual void OnSuccess()
    {
        GameManager.Instance.MoneyManager.Buy(price);
        gameObject.SetActive(false);
    }

    private void UpdateGauge()
    {
        elapsedTime += Time.deltaTime;
        var lerpValue = Mathf.Lerp(0f, 1f, elapsedTime / DURATION);
        SetProgressBar(lerpValue);
    }

    private void SetProgressBar(float value)
    {
        meshRenderer.materials[(int)PROGRESS_BAR.FRONT].SetFloat(FILL_AMOUNT, value);
    }

    private float GetProgressBar()
    {
        return meshRenderer.materials[(int)PROGRESS_BAR.FRONT].GetFloat(FILL_AMOUNT);
    }
}
