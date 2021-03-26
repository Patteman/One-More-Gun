using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";

    public SceneFader sceneFader;

    //Loads the main scene when called, and unpauses the game, incase it was paused
    public void Play()
    {
        sceneFader.FadeToScene(levelToLoad);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Exiting game...");
        //Closes the application
        Application.Quit();
    }
}
