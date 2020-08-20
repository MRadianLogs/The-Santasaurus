using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    static List<GameObject> gameItems = null;

    // Start is called before the first frame update
    void Start()
    {
        gameItems = new List<GameObject>();
             
        foreach(Transform child in transform)
        {
            gameItems.Add(child.gameObject);
        }
        //gameItems.AddRange(GetComponents<GameObject>());
    }

    public static List<GameObject> getGameItems()
    {
        return gameItems;
    }
}
