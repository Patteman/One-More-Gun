using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";

    public SceneFader sceneFader;
    public GameObject mainMenuObject;
    public GameObject settingsMenuObject;

    //public bool isMainMenu;

    [Header("Resolutions")]
    public Resolution[] resolutionsArray;

    void Start()
    {
        //May or may not be readded based on available time
        /*if (isMainMenu)
        {
            resolutionsArray = Screen.resolutions;
            SwapMenu(false);

            foreach (var res in resolutionsArray)
            {
                Debug.Log(res.width + "x" + res.height + " : " + res.refreshRate);
            }
        }*/
    }

    private void SetDropDownContents()
    {
       
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
