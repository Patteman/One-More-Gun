using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;

    //Restarts the current scene
    public void TryAgain()
    {
        sceneFader.FadeToScene(SceneManager.GetActiveScene().name);
    }

    //Returns to the menu scene using the scenefader class
    public void ToMenu()
    {
        sceneFader.FadeToScene(menuSceneName);
    }
}
