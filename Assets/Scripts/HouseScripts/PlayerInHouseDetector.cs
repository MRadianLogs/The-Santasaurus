using UnityEngine;

public class PlayerInHouseDetector : MonoBehaviour
{
    [SerializeField] private House house = null;

    public House GetHouse()
    {
        return house;
    }
}
