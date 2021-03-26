using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    void Update()
    {
        //Toggles the pause menu when pressing the escape or p key
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            ToggleMenu();
        }
    }

    //Makes the ui the opposite of its current state
    public void ToggleMenu()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        //Sets the timescale to 0, makes the "speed" at which the game is running,
        //stand still and the frames stop updating
        if (pauseUI.activeSelf) //(If self was activated)
        {
            Time.timeScale = 0f;
        }
        else //Set it to 1, i.e. normal time
        {
            Time.timeScale = 1f;
        }
    }

    //Restarts the scene at the current buildindex, i.e. the current active scene, and closes the menu
    //Uses the scenefader class to do this
    public void Restart()
    {
        ToggleMenu();
        sceneFader.FadeToScene(SceneManager.GetActiveScene().name);
    }

    //Returns to the menu scene using the scenefader class
    public void Menu()
    {
        ToggleMenu();
        sceneFader.FadeToScene(menuSceneName);
    }
}
