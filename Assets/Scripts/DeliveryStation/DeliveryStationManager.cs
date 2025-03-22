using UnityEngine;

public class DeliveryStationManager : MonoBehaviour
{
    [SerializeField]
    private ChickenDeliveryTrigger deliveryTrigger;

    public bool IsHaveChicken { get { return ChickenCount > 0; } }
    public int ChickenCount { get { return deliveryTrigger.Count; } }

    public Chicken GetChicken()
    {
        return deliveryTrigger.GetChicken();
    }
}
