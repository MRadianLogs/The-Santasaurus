using UnityEngine;

public class HouseManager : Manager
{
    public static HouseManager instance;

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

    public override void SetupList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            House childType = child.GetComponentInChildren<House>();
            if (childType != null)
            {
                AddItemToList(childType.GetHouseNum(), child);
            }
        }
    }

    public void AddNoiseToHouse(int houseNum, float numNoiseToAdd)
    {
        GetItemList()[houseNum].GetComponentInChildren<House>().AddNoise(numNoiseToAdd);
    }
}
