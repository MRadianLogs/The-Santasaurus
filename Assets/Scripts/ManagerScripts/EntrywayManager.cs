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
        foreach (Entryway entryway in transform.GetComponentsInChildren<Entryway>())
        {
            GameObject child = entryway.gameObject;
            if (entryway != null)
            {
                entryway.SetEntrywayNum(GetItemList().Count);
                AddItemToList(entryway.GetEntrywayNum(), child);
            }
        }
    }
}
