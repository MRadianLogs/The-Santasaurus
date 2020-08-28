using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuFunctions : MonoBehaviour
{
    private bool gameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI = null;

    private void Start()
    {
        if(GameManager.instance != null)
            GameManager.instance.OnGameEnded += HandleOnGameEnded;
        if(PauseController.instance != null)
        {
            PauseController.instance.OnGamePaused += HandleGamePaused;
            PauseController.instance.OnGameResumed += HandleGameResumed;
        }
        gameIsPaused = false;    
    }

    private void HandleOnGameEnded()
    {
        if(gameIsPaused)
        {
            HideUI();
        }
    }

    public void HandleGamePaused()
    {
        ShowUI();//Brings up menu.
        gameIsPaused = true;
    }

    public void HandleGameResumed()
    {
        HideUI();
        gameIsPaused = false;
    }

    private void ShowUI()
    {
        pauseMenuUI.SetActive(true);
    }
    private void HideUI()
    {
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
