using UnityEngine;

public class DeliveryStationManager : MonoBehaviour
{
    [SerializeField]
    private ChickenDeliveryTrigger deliveryTrigger;

    public bool IsHaveChicken { get { return deliveryTrigger.Count > 0; } }

    public Chicken GetChicken()
    {
        return deliveryTrigger.GetChicken();
    }
}
