using System.Collections;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    private GameObject currentHeldItem = null;
    public bool IsHoldingItem { get; private set; }

    [SerializeField] private float pickupCooldownTime = .5f;
    private bool canPickupItems = true;

    public bool PickUpItem(GameObject newItem)
    {
        if(canPickupItems)
        {
            currentHeldItem = newItem;
            IsHoldingItem = true;
            return true;
        }
        return false;
    }

    public void DropCurrentHeldItem()
    {
        currentHeldItem.transform.position = new Vector2(transform.position.x, transform.position.y);
        currentHeldItem.SetActive(true);
        currentHeldItem.GetComponentInChildren<PickupableItem>().GetDropped();

        //If item dropped inside of house,
        House itemHouse = PlayerHouseDetectionController.instance.GetCurrentActiveHouse();
        if (itemHouse != null)
        {
            itemHouse.HandleItemDroppedInHouse();
        }

        //Clear inventory.
        currentHeldItem = null;
        IsHoldingItem = false;
        //Start cooldown.
        StartCoroutine(StartPickupCoolDown());
    }

    private IEnumerator StartPickupCoolDown()
    {
        canPickupItems = false;
        yield return new WaitForSeconds(pickupCooldownTime);
        canPickupItems = true;
    }

}
