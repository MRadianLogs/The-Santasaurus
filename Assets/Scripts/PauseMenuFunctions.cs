using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuFunctions : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {
        gameIsPaused = false;    
    }

    void Update()
    { ///TODO: Change this to work with input manager.
        if (!GameManager.instance.GetGameHasEnded())
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (gameIsPaused)
                {
                    resume();
                }
                else
                {
                    pause();
                }
            }
        }
    }

    public void pause()
    {
        pauseMenuUI.SetActive(true);//Brings up menu.
        Time.timeScale = 0f; //Stops game.
        gameIsPaused = true;
    }

    public void resume()
    {
        pauseMenuUI.SetActive(false);//Hides menu.
        Time.timeScale = 1f; //Resumes game.
        gameIsPaused = false;
    }

    public void quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
