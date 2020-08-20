using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : Interactable
{
    public string itemName;//The name of the item.
    public int pointValue;//How much putting this item under the correct tree gives.

    public int treeDestinationNumber;//The number corresponding to the correct tree of which it should be placed under.
    bool isPickedUp;//Whether the item is currently in the dinos mouth.
    bool isInCorrectDestination;//Whether or not the item is under the correct tree.

    public override void interact()
    {
        //base.interact();//Calls the super method.

        pickUpItem();
    }

    void pickUpItem()
    {
        if (Time.time > PlayerInteraction.getNextPickUpTime())
        {
            if (PlayerInteraction.getItemInMouth() == null)
            {
                PlayerInteraction.setItemInMouth(this.gameObject);
                PlayerInteraction.setNextPickUpTime(Time.time + PlayerInteraction.getCoolDownTime());
                isInCorrectDestination = false;
            }
        }
    }

    public int getTreeDestinationNumber()
    {
        return treeDestinationNumber;
    }

    public void setIsInCorrectDestination(bool value)
    {
        this.isInCorrectDestination = value;
    }

    public bool getIsInCorrectDestination()
    {
        return isInCorrectDestination;
    }

    public int getPointValue()
    {
        return pointValue;
    }
}
