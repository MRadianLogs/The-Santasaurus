using System;
using System.Collections;
using UnityEngine;

public class Entryway : Interactable
{
    [SerializeField] private int houseNum = -1;
    [SerializeField] private float interactNoiseLevel = -1f; //How much noise using this item creates.
    [SerializeField] private GameObject door = null; //The actual object that disappears when the player interacts with it.
    private bool currentlyOpened = false;
    public event Action OnEntrywayOpened = delegate { };
    public event Action OnEntrywayClosed = delegate { };
    [SerializeField] private float timeBeforeAutoClose = 3f;

    private bool coroutineRunning = false;
    private Coroutine runningRoutine = null;

    public override void Interact()
    {
        if(!currentlyOpened)
        {
            OpenEntryway();
            if(coroutineRunning)
            {
                StopCoroutine(runningRoutine);
                coroutineRunning = false;
            }
            runningRoutine = StartCoroutine(TimedEntrywayAutoShut());
        }
        else
        {
            CloseEntryway();
        }
    }

    private void OpenEntryway()
    {
        door.SetActive(false);
        currentlyOpened = true;
        OnEntrywayOpened();
        //Add noise to house sneak meter.
        HouseManager.instance.AddNoiseToHouse(houseNum, interactNoiseLevel);
    }

    private void CloseEntryway()
    {
        door.SetActive(true);
        currentlyOpened = false;
        OnEntrywayClosed();
        //Add noise to house sneak meter.
    }

    private IEnumerator TimedEntrywayAutoShut()
    {
        coroutineRunning = true;
        //Debug.Log("Timer started!");
        yield return new WaitForSeconds(timeBeforeAutoClose);
        if(currentlyOpened)
        {
            CloseEntryway();
        }
        //Debug.Log("Auto closed!");
        coroutineRunning = false;
    }
}
