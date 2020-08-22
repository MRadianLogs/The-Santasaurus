using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHouseDetectionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerNearHouseDetector nearHouseDetector = collision.gameObject.GetComponentInChildren<PlayerNearHouseDetector>();
        if(nearHouseDetector != null)
        {
            //Near house.
            nearHouseDetector.GetHouse().SetPlayerIsNearHouse(true);
        }
        PlayerInHouseDetector inHouseDetector = collision.gameObject.GetComponentInChildren<PlayerInHouseDetector>();
        if(inHouseDetector != null)
        {
            //In House.
            inHouseDetector.GetHouse().SetPlayerIsInHouse(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerNearHouseDetector nearHouseDetector = collision.gameObject.GetComponentInChildren<PlayerNearHouseDetector>();
        if (nearHouseDetector != null)
        {
            //No longer near house.
            nearHouseDetector.GetHouse().SetPlayerIsNearHouse(false);
        }
        PlayerInHouseDetector inHouseDetector = collision.gameObject.GetComponentInChildren<PlayerInHouseDetector>();
        if (inHouseDetector != null)
        {
            //No longer in House.
            inHouseDetector.GetHouse().SetPlayerIsInHouse(false);
        }
    }
}
