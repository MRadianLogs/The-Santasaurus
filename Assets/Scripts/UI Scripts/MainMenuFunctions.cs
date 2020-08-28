using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void quit()
    {
        Application.Quit();//Exits the game.
    }
}
