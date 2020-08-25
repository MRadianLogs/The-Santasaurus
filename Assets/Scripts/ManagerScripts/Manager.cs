using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    private Dictionary<int, GameObject> itemList = null;

    public void Awake()
    {
        itemList = new Dictionary<int, GameObject>();
        SetupList();
    }

    public virtual void SetupList() { }

    public Dictionary<int, GameObject> GetItemList()
    {
        return itemList;
    }

    public void AddItemToList(int newItemNum, GameObject newItem)
    {
        itemList.Add(newItemNum, newItem);
    }

    public void RemoveItemFromList(int itemNum)
    {
        if (itemList[itemNum] != null)
        {
            itemList.Remove(itemNum);
        }
    }
}
