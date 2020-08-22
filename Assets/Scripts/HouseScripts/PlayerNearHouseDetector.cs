using UnityEngine;

public class PlayerNearHouseDetector : MonoBehaviour
{
    [SerializeField] private House house = null;

    public House GetHouse()
    {
        return house;
    }
}
