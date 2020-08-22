using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Manager
{
    public static ItemManager instance;

    private List<int> usedRandomNumbers = null;

    public new void Awake()
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

        base.Awake();
    }

    private void Start()
    {
        RandomizeItemDestinations(); //TODO: Combine these to save some frames.
        SetupItemList();
    }

    private void SetupItemList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            PickupableItem childType = child.GetComponentInChildren<PickupableItem>();
            if (childType != null)
            {
                AddItemToList(childType.GetDestNum(), child);
            }
        }
    }

    private void RandomizeItemDestinations()
    {
        usedRandomNumbers = new List<int>();

        int numDestinations = DestinationManager.instance.GetItemList().Count;
        for (int i = 0; i < transform.childCount; i++)//foreach (GameObject item in GetItemList().Values)
        {
            int randomDestNum = Random.Range(0, numDestinations);
            Debug.Log("First random num: " + randomDestNum);
            while (usedRandomNumbers.Contains(randomDestNum))
            {
                randomDestNum = Random.Range(0, numDestinations);
                Debug.Log("new rand: " + randomDestNum);
            }
            transform.GetChild(i).GetComponentInChildren<PickupableItem>().SetDestNum(randomDestNum);
            usedRandomNumbers.Add(randomDestNum);
        }
    }
}
