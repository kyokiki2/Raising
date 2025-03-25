using System.Collections;
using UnityEngine;

public class BaseTrigger : MonoBehaviour
{
    protected CharacterBase character = null;
    private Coroutine updateCoroutine = null;

    private void OnTriggerEnter(Collider other)
    {
        character = other.GetComponent<CharacterBase>();
        if (character == null)
            return;

        OnEnter();

        ClearUpdate();

        updateCoroutine = StartCoroutine(DoUpdate());
    }


    private void OnTriggerExit(Collider other)
    {
        if (character == null)
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
