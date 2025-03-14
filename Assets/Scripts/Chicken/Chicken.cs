using UnityEngine;

public class Chicken : MonoBehaviour
{
    public bool IsActive { get { return gameObject.activeSelf; } 
                           set { gameObject.SetActive(value); } }
}
