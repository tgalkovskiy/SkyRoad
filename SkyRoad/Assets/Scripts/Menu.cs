using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// Restart Game
    /// </summary>
    public void RestartGame()
    {
        //Load Now Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Start Game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Exit Game
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
