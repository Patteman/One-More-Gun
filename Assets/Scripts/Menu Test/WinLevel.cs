using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{
    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    //Restarts the current scene

    private void Start()
    {

    }

    public void TryAgain()
    {
        sceneFader.FadeToScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        sceneFader.FadeToScene("Game2");
        MainMenu.lvlOneDone = true;
    }

    //Returns to the menu scene using the scenefader class
    public void ToMenu()
    {
        sceneFader.FadeToScene(menuSceneName);
    }
}
