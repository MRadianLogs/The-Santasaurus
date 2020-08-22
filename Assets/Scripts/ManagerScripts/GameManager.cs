using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float timeLeftInGame = 240f;

    private float score = 0;
    private bool gameHasEnded = false;

    [SerializeField] private GameObject gameOverUI;

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
        score = 0;
        gameHasEnded = false;

        StartCoroutine(TickDownTime());
    }

    private IEnumerator TickDownTime()
    {
        while(!gameHasEnded)
        {
            yield return new WaitForSeconds(1f);
            timeLeftInGame -= 1f;
            CheckRoundEndConditions();
        }
    }

    private void CheckRoundEndConditions()
    {
        //Round shoudle end when player gets all gifts in the proper locations or time runs out.
        if(timeLeftInGame <= 0)//Time runs out.
        {
            //Stop game. 
            EndGame();
        }
        else if(ItemManager.instance.GetNumItemsInCorrectSpot() == ItemManager.instance.GetItemList().Count) //If all items in correct spot.
        {
            //Stop game.
            EndGame();
        }
    }

    private void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
            //Good job giving!
            //Calc points.
            CalcPoints();
            //Show game over screen.
            gameOverUI.SetActive(true);
        }
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
                Debug.Log("Adding to score!");
            }
        }
        Debug.Log("Total points: " + score);
    }

    public bool GetGameHasEnded()
    {
        return gameHasEnded;
    }
}
