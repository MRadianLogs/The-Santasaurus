using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entryway : Interactable
{
    [SerializeField] private int houseNum = -1;
    [SerializeField] private float interactNoiseLevel = -1f; //How much noise using this item creates.
    [SerializeField] private GameObject door = null; //The actual object that disappears when the player interacts with it.
    private bool currentlyOpened = false;

    public override void Interact()
    {
        if(!currentlyOpened)
        {
            door.SetActive(false);
            currentlyOpened = true;
            //Add noise to house sneak meter.
            //HouseManager.instance.AddNoiseToHouse(houseNum, interactNoiseLevel);
        }
        else
        {
            door.SetActive(true);
            currentlyOpened = false;
            //Add noise to house sneak meter.
            //HouseManager.instance.AddNoiseToHouse(houseNum, interactNoiseLevel);
        }
    }
}
