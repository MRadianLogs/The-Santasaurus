using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationManager : Manager
{
    public static DestinationManager instance;

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
            Destination childType = child.GetComponentInChildren<Destination>();
            if (childType != null)
            {
                AddItemToList(childType.GetDestNum(), child);
            }
        }
    }
}
