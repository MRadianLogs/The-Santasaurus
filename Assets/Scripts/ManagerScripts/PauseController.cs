using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController instance;

    private bool gameIsPaused = false;
    public event Action OnGamePaused = delegate { };
    public event Action OnGameResumed = delegate { };

    [SerializeField] private PlayerInputController inputController = null;

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

    private void Start()
    {
        gameIsPaused = false;
    }

    private void Update()
    {
        if (!GameManager.instance.GetGameHasEnded())
        {
            if (inputController.PauseButtonInput)
            {
                if (gameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void ResumeGame()
    {
        OnGameResumed();
        Time.timeScale = 1f; //Resumes game.
        gameIsPaused = false;
    }
    public void PauseGame()
    {
        OnGamePaused();
        Time.timeScale = 0f; //Stops game.
        gameIsPaused = true;
    }
}
