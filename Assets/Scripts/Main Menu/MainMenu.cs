using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void playGame()
    {
        SceneManager.LoadScene("Playground");
    }

    public void optionGame()
    {
        SceneManager.LoadScene("Option");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
