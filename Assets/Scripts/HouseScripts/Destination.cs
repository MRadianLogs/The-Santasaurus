using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private float itemPlacementRadius = 2f;//The radius that counts for items placed under tree.

    [SerializeField] private int destNum = -1;
    private List<GameObject> itemsUnderTree;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, itemPlacementRadius);
    }

    private void Awake()
    {
        itemsUnderTree = new List<GameObject>();
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        //Debug.Log("Collided with: " + collision.name);
        GameObject collidedObject = collision.gameObject;
        PickupableItem newItem = collidedObject.GetComponent<PickupableItem>();

        //Check if collided with a new item under tree. (item not in list)
        if (newItem != null)
        {
            //Add item to list.
            itemsUnderTree.Add(collidedObject);
            //Recalc if in proper location and mark item appropriately.
            CheckIfProperLocation(newItem);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (itemsUnderTree.Contains(collidedObject))
        {
            PickupableItem item = collidedObject.GetComponent<PickupableItem>();
            itemsUnderTree.Remove(collidedObject);
            //Debug.Log("Removed: " + item.name + " from list!");
        }
    }

    private void CheckIfProperLocation(PickupableItem item)
    {
        if(item.GetDestNum() == destNum)
        {
            Debug.Log("" + item.name + " is in the right place!");
            item.SetIsInCorrectDest(true);
        }
    }

    public int GetDestNum()
    {
        return destNum;
    }
}
