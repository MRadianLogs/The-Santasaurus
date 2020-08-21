using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for detecting whether or not there is an interactable object near the player. This is done using colliders.
/// </summary>
public class PlayerInteractionDetectionController : MonoBehaviour
{
    private List<GameObject> nearbyInteractableObjects = null;
    [SerializeField] private Transform playerPosition = null;

    private void Awake()
    {
        nearbyInteractableObjects = new List<GameObject>();
    }

    public GameObject GetClosestInteractableObject()
    {
        if(nearbyInteractableObjects.Count < 1)
        {
            return null;
        }
        else if(nearbyInteractableObjects.Count == 1)
        {
            return nearbyInteractableObjects[0];
        }
        else //Multiple objects.
        {
            //Find closest object to player.
            float closestDistance = Mathf.Infinity;
            GameObject closestObject = nearbyInteractableObjects[0];
            foreach (GameObject interactableObject in nearbyInteractableObjects)
            {
                float newDistance = Vector2.Distance(interactableObject.transform.position, playerPosition.position);
                if(newDistance < closestDistance)
                {
                    closestDistance = newDistance;
                    closestObject = interactableObject;
                }
            }
            return closestObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If interactable object and not already in list, add to list.
        GameObject collidedObject = collision.gameObject;
        if(collidedObject.GetComponentInChildren<Interactable>() != null && !nearbyInteractableObjects.Contains(collidedObject))
        {
            nearbyInteractableObjects.Add(collidedObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If in list, remove from list.
        GameObject collidedObject = collision.gameObject;
        if (nearbyInteractableObjects.Contains(collidedObject))
        {
            nearbyInteractableObjects.Remove(collidedObject);
        }
    }
}
