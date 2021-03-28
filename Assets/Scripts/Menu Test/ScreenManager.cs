using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public bool gameEnded;
    public GameObject gameOverUI;
    public GameObject winLevelUI;

    void Start()
    {
        gameEnded = false;
    }

    void Update()
    {
        //If game has ended, do nothing
        if (gameEnded)
        {
            return;
        }

        //temp dev shortcut to end game
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        //temp dev shortcut to win game
        if (Input.GetKeyDown("q"))
        {
            WinGame();
        }
    }

    //Ends the game and activates the game over UI
    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    //Ends the game and activates the win screen UI
    public void WinGame()
    {
        gameEnded = true;
        winLevelUI.SetActive(true);
    }
}
