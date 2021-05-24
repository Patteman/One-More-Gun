using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";

    public SceneFader sceneFader;
    public GameObject mainMenuObject;
    public GameObject settingsMenuObject;

    void Start()
    {
        SwapMenu(false);
    }

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

    public void SwapMenu(bool enableSettingsMenu)
    {
        if (enableSettingsMenu)
        {
            settingsMenuObject.SetActive(true);
            mainMenuObject.SetActive(false);
        }
        else if (!enableSettingsMenu)
        {
            settingsMenuObject.SetActive(false);
            mainMenuObject.SetActive(true);
        }

    }
}
