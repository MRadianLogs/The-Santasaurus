using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script represents a house in the game. It will have entryways into the house, 
/// the main christmas tree to place presents under, a stealth meter for how asleep the npcs are, 
/// and the npcs that will chase the player when awake.
/// </summary>
public class House : MonoBehaviour
{
    [SerializeField] private int houseNum = -1;
    private float noiseMeter = -1f; //This determines how asleep the npcs of the house are. If this reaches too high, they wake up!
    [SerializeField] private GameObject houseTree = null;
    [SerializeField] private GameObject houseNPC = null;
    private List<GameObject> entryways = null;

    private void Awake()
    {
        entryways = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Entryway childType = child.GetComponentInChildren<Entryway>();
            if (childType != null)
            {
                entryways.Add(child);
            }
        }
    }

    public int GetHouseNum()
    {
        return houseNum;
    }

    public void AddNoise(float numNoise)
    {
        noiseMeter += numNoise;
    }
}
