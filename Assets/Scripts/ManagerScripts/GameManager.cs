﻿using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float timeLeftInGame = 240f;
    public event Action<float> OnTimeLeftValueChanged = delegate { };

    private float score = 0;
    private bool gameHasEnded = false;

    public event Action OnGameStarted = delegate { };
    public event Action OnHandleNoiseMeterFull = delegate { };
    public event Action OnTimeRanOut = delegate { };
    public event Action OnGameEnded = delegate { };

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
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject house in HouseManager.instance.GetItemList().Values)
        {
            House houseScript = house.GetComponentInChildren<House>();
            houseScript.OnNoiseMeterFull += HandleHouseNoiseMeterFull;
        }

        score = 0;
        gameHasEnded = false;
        OnGameStarted();
        Time.timeScale = 1;
        StartCoroutine(TickDownTime());
    }

    private IEnumerator TickDownTime()
    {
        OnTimeLeftValueChanged(timeLeftInGame);
        while (!gameHasEnded)
        {
            yield return new WaitForSeconds(1f);
            timeLeftInGame -= 1f;
            OnTimeLeftValueChanged(timeLeftInGame);
            CheckRoundEndConditions();
        }
    }

    private void CheckRoundEndConditions()
    {
        //Round shoudle end when player gets all gifts in the proper locations or time runs out.
        if(timeLeftInGame <= 0)//Time runs out.
        {
            OnTimeRanOut();
            //Stop game. 
            EndGame();
        }
        else if(ItemManager.instance.GetNumItemsInCorrectSpot() == ItemManager.instance.GetItemList().Count) //If all items in correct spot.
        {
            //Stop game.
            EndGame();
        }
    }

    private void HandleHouseNoiseMeterFull()
    {
        OnHandleNoiseMeterFull(); //A noise meter has filled.
        EndGame();
    }

    private void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            //Debug.Log("Game Over!");
            //Good job giving!
            //Calc points.
            CalcPoints();

            //Hide all other UI and show game over screen.
            OnGameEnded();
            StartCoroutine(StopTimeAfterEndGame());
        }
    }

    private IEnumerator StopTimeAfterEndGame()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;//Stops  game time.
    }

    private void CalcPoints()
    {
        //Loop through all items
        //If item is in destination, add points to score.
        foreach(GameObject itemObject in ItemManager.instance.GetItemList().Values)
        {
            PickupableItem item = itemObject.GetComponent<PickupableItem>();
            if(item.GetIsInCorrectDest())
            {
                score += item.GetPointValue();
                //Debug.Log("Adding to score!");
            }
        }
        //Debug.Log("Total points: " + score);
    }

    public bool GetGameHasEnded()
    {
        return gameHasEnded;
    }

    public float GetScore()
    {
        return score;
    }
}
