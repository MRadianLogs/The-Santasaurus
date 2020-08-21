using UnityEngine;

public class PickupableItem : Interactable
{
     public string ItemName { get; }//The name of the item.
    [SerializeField] public int PointValue { get; }//How much putting this item under the correct tree gives.

    public int TreeDestinationNumber { get; private set; }//The number corresponding to the correct tree of which it should be placed under.
    private bool isPickedUp = false;//Whether the item is currently in the dinos mouth.
    public bool IsInCorrectDestination { get; private set; }//Whether or not the item is under the correct tree.

    private PlayerInventoryController playerInventory = null;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventoryController>();
    }

    private void Start()
    {
        ItemManager.instance.gameItems.Add(gameObject);
    }

    public override void Interact()
    {
        //base.interact();//Calls the super method.
        if(playerInventory.PickUpItem(gameObject)) //If pickup successful.
        {
            GetPickedUp();
        }
    }

    public void GetPickedUp()
    {
        isPickedUp = true;
        IsInCorrectDestination = false;
        gameObject.SetActive(false);
    }

    public void GetDropped()
    {
        isPickedUp = false;
    }

    public void SetIsInCorrectDestination(bool newValue)
    {
        IsInCorrectDestination = newValue;
    }

    public void SetTreeDestinationNumber(int newDestNumber)
    {
        TreeDestinationNumber = newDestNumber;
    }
}
