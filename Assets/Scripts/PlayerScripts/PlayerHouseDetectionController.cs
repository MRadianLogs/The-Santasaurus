using UnityEngine;

public class PlayerHouseDetectionController : MonoBehaviour
{
    public static PlayerHouseDetectionController instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists! Destroying object!");
            Destroy(transform.root.gameObject);
        }
    }

    private House currentActiveHouse = null; //The house the player is currently inside.

    public House GetCurrentActiveHouse()
    {
        return currentActiveHouse;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerNearHouseDetector nearHouseDetector = collision.gameObject.GetComponentInChildren<PlayerNearHouseDetector>();
        if(nearHouseDetector != null)
        {
            //Near house.
            House relatedHouse = nearHouseDetector.GetHouse();
            relatedHouse.SetPlayerIsNearHouse(true);
            relatedHouse.ShowHouseNoiseMeterUI();
        }
        PlayerInHouseDetector inHouseDetector = collision.gameObject.GetComponentInChildren<PlayerInHouseDetector>();
        if(inHouseDetector != null)
        {
            //In House.
            House relatedHouse = inHouseDetector.GetHouse();
            relatedHouse.SetPlayerIsInHouse(true);
            currentActiveHouse = relatedHouse;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerNearHouseDetector nearHouseDetector = collision.gameObject.GetComponentInChildren<PlayerNearHouseDetector>();
        if (nearHouseDetector != null)
        {
            //No longer near house.
            House relatedHouse = nearHouseDetector.GetHouse();
            relatedHouse.SetPlayerIsNearHouse(false);
            relatedHouse.HideHouseNoiseMeterUI();

        }
        PlayerInHouseDetector inHouseDetector = collision.gameObject.GetComponentInChildren<PlayerInHouseDetector>();
        if (inHouseDetector != null)
        {
            //No longer in House.
            inHouseDetector.GetHouse().SetPlayerIsInHouse(false);
            currentActiveHouse = null;
        }
    }
}
