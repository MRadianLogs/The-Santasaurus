using UnityEngine;

public class PickupableItem : Interactable
{
    [SerializeField] private string itemName = "";//The name of the item.
    [SerializeField] private int pointValue = -1;//How much putting this item under the correct tree gives.

    [SerializeField] private int destNum = -1;//The number corresponding to the correct tree of which it should be placed under.
    private bool isPickedUp = false;//Whether the item is currently in the dinos mouth.
    private bool isInCorrectDest = false;//Whether or not the item is under the correct tree.

    private PlayerInventoryController playerInventory = null;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventoryController>();
    }

    public override void Interact()
    {
        //base.interact();//Calls the super method.
        if(playerInventory.PickUpItem(gameObject)) //If pickup successful.
        {
            GetPickedUp();
        }
    }

    public string GetItemName()
    {
        return itemName;
    }

    public int GetPointValue()
    {
        return pointValue;
    }

    public int GetDestNum()
    {
        return destNum;
    }
    public void SetDestNum(int newValue)
    {
        destNum = newValue;
    }

    public bool GetIsPickedUp()
    {
        return isPickedUp;
    }

    public void GetPickedUp()
    {
        isPickedUp = true;
        SetIsInCorrectDest(false);
        gameObject.SetActive(false);
    }

    public void GetDropped()
    {
        isPickedUp = false;
    }

    public bool GetIsInCorrectDest()
    {
        return isInCorrectDest;
    }
    public void SetIsInCorrectDest(bool newValue)
    {
        if(isInCorrectDest) //If item was already in correct location,
        {
            ItemManager.instance.DecrementNumitemsInCorrectSpot();
        }
        isInCorrectDest = newValue;
        if(isInCorrectDest) //If item is now in correct location,
        {
            ItemManager.instance.IncrementNumItemsInCorrectSpot();
        }
    }
}
