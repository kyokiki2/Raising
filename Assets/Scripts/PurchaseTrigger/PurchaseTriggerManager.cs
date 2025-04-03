using UnityEngine;

public class PurchaseTriggerManager : MonoBehaviour
{
    [SerializeField]
    private PurchaseTrigger[] purchaseTriggers;

    public void Init()
    {
        for(int i = 0; i < purchaseTriggers.Length; ++i)
        {
            purchaseTriggers[i].Init();
        }
    }
}
