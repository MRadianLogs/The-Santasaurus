using UnityEngine;

/// <summary>
/// This script is responsible for responding to the player pressing the interact button, 
/// by having the world respond in some ways, such as picking up or dropping an item, or opening and closing something.
/// </summary>
public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private PlayerInputController inputController = null;

    [SerializeField] private PlayerInventoryController inventoryController = null;

    [SerializeField] private PlayerInteractionDetectionController detectionController = null;

    private void Awake()
    {
        if(inputController != null)
        {
            inputController.OnInteract += Interact; //When this event is thrown, respond with the Interact method.
        }
    }

    /// <summary>
    /// If the player has something in their mouth, drop it. Or, if they are facing something that can be interacted with, interact with that object.
    /// </summary>
    private void Interact()
    {
        if(inventoryController != null)
        {
            if(inventoryController.IsHoldingItem)
            {
                inventoryController.DropCurrentHeldItem();
            }
            else
            {
                //Check whats in front of player, if there is interactable, such as an item or door. Interact with it.
                GameObject interactableObject = detectionController.GetClosestInteractableObject();
                if (interactableObject != null)
                {
                    //Debug.Log("Closest interable object: " + interactableObject.name);
                    interactableObject.GetComponentInChildren<Interactable>().Interact();
                }
            }
        }
    }
}
