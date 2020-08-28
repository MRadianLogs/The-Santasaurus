using UnityEngine;

public class EntrywayManager : Manager
{
    public static EntrywayManager instance;

    private new void Awake()
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
            Entryway childType = child.GetComponentInChildren<Entryway>();
            if (childType != null)
            {
                childType.SetEntrywayNum(GetItemList().Count);
                AddItemToList(childType.GetEntrywayNum(), child);
            }
        }
    }
}
