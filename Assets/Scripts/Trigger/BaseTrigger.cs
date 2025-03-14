using System.Collections;
using UnityEngine;

public class BaseTrigger : MonoBehaviour
{
    protected bool IsPlayer(Collider other) { return other.CompareTag(Tag.PLAYER); }
    private Coroutine updateCoroutine = null;

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other) == false)
            return;

        OnEnter();

        ClearUpdate();

        updateCoroutine = StartCoroutine(DoUpdate());
    }


    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other) == false)
            return;

        ClearUpdate();

        OnExit();
    }

    private IEnumerator DoUpdate()
    {
        while (true)
        {
            OnUpdate();
            yield return null;
        }
    }

    private void ClearUpdate()
    {
        if (updateCoroutine == null)
            return;

        StopCoroutine(updateCoroutine);
        updateCoroutine = null;
    }

    protected virtual void OnEnter()
    {

    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnExit()
    {

    }
}
