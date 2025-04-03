using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrigger : MonoBehaviour
{
    protected List<CharacterBase> charList = new();
    private Coroutine updateCoroutine = null;

    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<CharacterBase>();
        if (character == null)
            return;

        if (IsEnter(character) == false)
            return;

        OnEnter(character);
    }

    private void OnTriggerExit(Collider other)
    {
        var character = other.GetComponent<CharacterBase>();
        if (character == null)
            return;

        OnExit(character);
    }

    protected virtual bool IsEnter(CharacterBase character)
    {
        return true;
    }


    private IEnumerator DoUpdate()
    {
        while (true)
        {
            OnUpdate();
            yield return null;
        }
    }

    private void StartUpdate()
    {
        if (updateCoroutine != null)
            return;

        updateCoroutine = StartCoroutine(DoUpdate());
    }

    private void EndUpdate()
    {
        if (updateCoroutine == null)
            return;

        StopCoroutine(updateCoroutine);
        updateCoroutine = null;
    }

    protected virtual void OnEnter(CharacterBase character)
    {
        charList.Add(character);

        if (charList.Count > 0)
            StartUpdate();
    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnExit(CharacterBase character)
    {
        charList.Remove(character);

        if (charList.Count <= 0)
            EndUpdate();
    }
}
