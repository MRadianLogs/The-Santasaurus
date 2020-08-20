using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuFunctions : MonoBehaviour
{
    public void replay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
