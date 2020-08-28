using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private Text scoreText = null;

    private void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.OnGameEnded += HandleGameEnded;
        }
    }

    private void HandleGameEnded()
    {
        //Set score.
        scoreText.text = GameManager.instance.GetScore().ToString();
        //Show game over screen.
        gameOverScreen.SetActive(true);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
