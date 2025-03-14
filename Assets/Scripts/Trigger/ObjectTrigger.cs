using System.Collections;
using UnityEngine;

public class ObjectTrigger : BaseTrigger
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    private const float DURATION = 3f;
    private float elapsedTime = 0f;
    private bool isGauageFull = false;

    private enum PROGRESS_BAR
    {
        BACK = 0,
        FRONT = 1
    }


    protected override void OnEnter()
    {
        base.OnEnter();

        isGauageFull = false;
        elapsedTime = 0f;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if(isGauageFull == false)
        {
            UpdateGauage();
            isGauageFull = GetProgressBar() == 1f;
            if (isGauageFull)
                OnSuccess();
        }
    }

    protected override void OnExit()
    {
        base.OnExit();

        SetProgressBar(0f);
    }

    protected virtual void OnSuccess()
    {
        Debug.LogError("Success");
    }

    private void UpdateGauage()
    {
        elapsedTime += Time.deltaTime;
        var lerpValue = Mathf.Lerp(0f, 1f, elapsedTime / DURATION);
        SetProgressBar(lerpValue);
    }

    private void SetProgressBar(float value)
    {
        meshRenderer.materials[(int)PROGRESS_BAR.FRONT].SetFloat("_FillAmount", value);
    }

    private float GetProgressBar()
    {
        return meshRenderer.materials[(int)PROGRESS_BAR.FRONT].GetFloat("_FillAmount");
    }

}
