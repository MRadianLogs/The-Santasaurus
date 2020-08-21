using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public List<GameObject> gameItems { get; private set; }
    private List<int> usedRandomNumbers = null;

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

        gameItems = new List<GameObject>();
        usedRandomNumbers = new List<int>();
    }

    private void Start()
    {
        RandomizeItemDestinations();
    }

    private void RandomizeItemDestinations()
    {
        int numDestinations = DestinationManager.instance.destinationTrees.Count;
        foreach (GameObject item in gameItems)
        {
            int randomDestNum = Random.Range(0, numDestinations-1);
            Debug.Log("First random num: " + randomDestNum);
            while (usedRandomNumbers.Contains(randomDestNum)) //TODO: FIX INFINITE LOOP ERROR! Have this done last!
            {
                randomDestNum = Random.Range(0, numDestinations - 1);
                Debug.Log("new rand: " + randomDestNum);
            }
            item.GetComponentInChildren<PickupableItem>().SetTreeDestinationNumber(randomDestNum);
            usedRandomNumbers.Add(randomDestNum);
        }
    }
}
