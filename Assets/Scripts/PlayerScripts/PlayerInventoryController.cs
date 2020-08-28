using System;
using System.Collections;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    public static PlayerInventoryController instance;

    private GameObject currentHeldItem = null;
    public bool IsHoldingItem { get; private set; }
    public event Action OnItemPickedUp = delegate { };
    public event Action OnItemDropped = delegate { };

    [SerializeField] private float pickupCooldownTime = .5f;
    private bool canPickupItems = true;

    private void Awake()
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

    public bool PickUpItem(GameObject newItem)
    {
        if(canPickupItems)
        {
            currentHeldItem = newItem;
            OnItemPickedUp();
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
        OnItemDropped();

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
