using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuFunctions : MonoBehaviour
{
    private bool gameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI = null;

    [SerializeField] private PlayerInputController inputController = null;

    private void Start()
    {
        GameManager.instance.OnGameEnded += HandleOnGameEnded;

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

    private void HandleOnGameEnded()
    {
        if(gameIsPaused)
        {
            HideUI();
        }
    }

    public void PauseGame()
    {
        ShowUI();//Brings up menu.
        Time.timeScale = 0f; //Stops game.
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        HideUI();
        Time.timeScale = 1f; //Resumes game.
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
