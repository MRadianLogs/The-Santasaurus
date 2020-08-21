using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationManager : MonoBehaviour
{
    public static DestinationManager instance;

    public List<GameObject> destinationTrees { get; private set; }

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

        destinationTrees = new List<GameObject>();
    }
}
