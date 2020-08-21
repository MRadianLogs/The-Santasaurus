using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static float currentTime;
    static float startTime = 240;//How long the game should last, in seconds.

    static float score = 0;
    static bool gameHasEnded = false;

    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameHasEnded = false;
        startTime = 240;
        currentTime = startTime;        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        checkRoundEndConditions();
    }

    private void checkRoundEndConditions()
    {
        //Round shoudle end when player gets all gifts in the proper locations or time runs out.
        if(currentTime <= 0)//Time runs out.
        {
            //Stop game. 
            endGame();
            //Calculate points.
        }
    }

    private void endGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
            //Good job giving!
            //Calc points.
            calcPoints();
            //Show start screen.
            gameOverUI.SetActive(true);
        }
    }

    private void calcPoints()
    {
        //Loop through all items
        //If item is in destination, add points to score.
        foreach(GameObject itemObject in ItemManager.instance.gameItems)
        {
            PickupableItem item = itemObject.GetComponent<PickupableItem>();
            if(item.IsInCorrectDestination)
            {
                score += item.PointValue;
                Debug.Log("Adding to score!");
            }
        }
        Debug.Log("Total points: " + score);
    }

    public static bool getGameHasEnded()
    {
        return gameHasEnded;
    }
}
