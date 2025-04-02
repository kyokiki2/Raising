using UnityEngine;

public class ChickenDeliveryManager : MonoBehaviour
{
    [SerializeField]
    private ChickenDeliveryTrigger deliveryTrigger;

    public bool IsHaveChicken { get { return ChickenCount > 0; } }
    public int ChickenCount { get { return deliveryTrigger.Count; } }
    public Vector3 CharTargetPos { get { return deliveryTrigger.CharTargetPos; } }

    public Chicken GetChicken()
    {
        return deliveryTrigger.GetChicken();
    }
}
