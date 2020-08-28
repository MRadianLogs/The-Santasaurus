using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Manager
{
    public static ItemManager instance;

    [SerializeField] private GameObject itemPrefab = null;
    [SerializeField] private int numItemsToSpawn = 6;

    [SerializeField] private int numDests = 6;
    private List<int> usedRandomDestNumbers = null;

    [SerializeField] private int numSprites = 6;
    private List<int> usedRandomSpriteNums = null;

    private List<int> usedRandomTreeNumbers = null;

    private int numItemsInCorrectSpot = -1;

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
        SetupItems();
    }

    private void SetupItems()
    {
        usedRandomDestNumbers = new List<int>();
        usedRandomSpriteNums = new List<int>();
        usedRandomTreeNumbers = new List<int>();

        //Instantiate all items needed.
        //Randomize item destinations.
        //Randomize item sprites.
        //Place items in random trees.
        for (int i = 0; i < numItemsToSpawn; i++)
        {
            //Instantiate new item.
            GameObject newItem = Instantiate(itemPrefab);
            PickupableItem newItemScript = newItem.GetComponentInChildren<PickupableItem>();
            //Randomize item destination.
            RandomizeItemDestination(newItemScript);
            //Randomize item sprite.
            RandomizeItemSprite(newItemScript);
            //Assign sprite to dest house.
            HouseManager.instance.GetItemList()[newItemScript.GetDestNum()].GetComponentInChildren<House>().SetWantedItemSprite(ItemSpriteManager.instance.GetSprite(newItemScript.GetSpriteNum()));
            //Place item in random tree.
            RandomizeItemTreeSpawnPosition(newItemScript);
            AddItemToList(i, newItem);
        }
    }

    private void RandomizeItemDestination(PickupableItem newItem)
    {
        newItem.SetDestNum(GetUnusedRandomNumber(numDests, usedRandomDestNumbers));
    }

    private void RandomizeItemSprite(PickupableItem newItem)
    {
        newItem.SetSpriteNum(GetUnusedRandomNumber(numSprites, usedRandomSpriteNums));
    }

    private int GetUnusedRandomNumber(int randomRangeMax, List<int> usedNumbersList)
    {
        int randomNum = Random.Range(1, randomRangeMax+1);
        while (usedNumbersList.Contains(randomNum))
        {
            randomNum = Random.Range(1, randomRangeMax+1);
        }
        usedNumbersList.Add(randomNum);
        return randomNum;
    }

    private void RandomizeItemTreeSpawnPosition(PickupableItem newItem)
    {
        newItem.SetSpawnPosition(DestinationManager.instance.GetItemList()[GetUnusedRandomNonDestTreeNumber(numDests, usedRandomTreeNumbers, newItem.GetDestNum())].transform.position);
    }

    private int GetUnusedRandomNonDestTreeNumber(int randomRangeMax, List<int> usedNumbersList, int destNum)
    {
        int randomNum = Random.Range(1, randomRangeMax+1);
        while (usedNumbersList.Contains(randomNum)) //|| randomNum == destNum) ///TODO: This has a chance of infinite looping. Find a fix?
        {
            randomNum = Random.Range(1, randomRangeMax+1);
        }
        usedNumbersList.Add(randomNum);
        return randomNum;
    }

    public void IncrementNumItemsInCorrectSpot()
    {
        numItemsInCorrectSpot++;
    }
    public void DecrementNumitemsInCorrectSpot()
    {
        numItemsInCorrectSpot--;
    }
    public int GetNumItemsInCorrectSpot()
    {
        return numItemsInCorrectSpot;
    }
}
