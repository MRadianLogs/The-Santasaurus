using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public float itemPlacementRadius = 2f;//The radius that counts for items placed under tree. Should I use a collider instead?

    public int destinationNumber;
    List<GameObject> itemsUnderTree;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, itemPlacementRadius);
    }

    // Start is called before the first frame update
    void Start()
    {
        itemsUnderTree = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if any items removed. (Out of range).
        checkAnyItemsRemoved();
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.name);
        GameObject collidedObject = collision.gameObject;
        PickupableItem item = collidedObject.GetComponent<PickupableItem>();

        //Check if collided with a new item under tree. (item not in list)
        if(item != null)
        {
            //Add item to list.
            itemsUnderTree.Add(collidedObject);
            //Recalc if in proper location and mark item appropriately.
            checkProperLocation();
        }
    }

    private void checkAnyItemsRemoved()
    {
        //Debug.Log("Checking if any items removed.");
        for(int i = 0; i < itemsUnderTree.Count; i++)
        {
            GameObject itemGameObject = itemsUnderTree[i];
            float distance = Vector2.Distance(itemGameObject.transform.position, transform.position);
            if (distance > itemPlacementRadius)
            {
                //Remove from list.
                itemsUnderTree.Remove(itemGameObject);
                Debug.Log("Removed: " + itemGameObject.name + " from list!");
            }
        }
    }

    private void checkProperLocation()
    {
        foreach(GameObject itemGameObject in itemsUnderTree)
        {
            PickupableItem item = itemGameObject.GetComponent<PickupableItem>();
            if(item.getTreeDestinationNumber() == destinationNumber)
            {
                Debug.Log("" + item.name + " is in the right place!");
                item.setIsInCorrectDestination(true);
            }
        }
    }
}
