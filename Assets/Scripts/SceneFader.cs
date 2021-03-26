using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    //Starts the coroutine when starting the game
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    //Starts the coroutine when called
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    //Coroutine used to fade in
    IEnumerator FadeIn()
    {
        float t = 1f;

        //Makes t decrease by a little bit until next frame
        //And decrease the aplha value by the time and curve assigned
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        
    }

    //Coroutine used to fade out AND change the scene
    IEnumerator FadeOut(string sceneName)
    {
        float t = 0f;

        //Makes t decrese by a little bit until next frame
        //And increase the aplha value by the time and curve assigned
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        //Changes the scene to the main level
        SceneManager.LoadScene(sceneName);
    }
}
