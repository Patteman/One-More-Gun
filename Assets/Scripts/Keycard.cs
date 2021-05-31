using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public GameObject door;
    private bool coroutineAllowed;

    private void Start()
    {
        coroutineAllowed = true;
    }

    private void Update()
    {
        //Checks if the coroutine is already started or not. If it has not started yet, then start coroutine.
        if (coroutineAllowed)
        {
            StartCoroutine("StartPulsing");
        }
    }

    //Checks if player moved over keycard, if so, open the assigned door, and remove the keycard.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            door.SetActive(false);
            Debug.Log("Keycard picked up");
            Destroy(gameObject);
        }
    }

    //Coroutine to make the keycard "pulse" so it is easy to differentiate from the background aesthetics.
    private IEnumerator StartPulsing()
    {
        //Makes sure the coroutine cannot be started again before the code has been run fully.
        coroutineAllowed = false;
        
        //Makes the scale increase by 0.025f 10 times, every 0.05f secounds.
        for (float i = 0f; i <= 1f; i+=0.1f)
        {
            transform.localScale = new Vector2(
            (Mathf.Lerp(transform.localScale.x, transform.localScale.x + 0.025f, Mathf.SmoothStep(0f, 1f, i))),
            (Mathf.Lerp(transform.localScale.y, transform.localScale.y + 0.025f, Mathf.SmoothStep(0f, 1f, i)))
            );
            yield return new WaitForSeconds(0.05f);
        }

        //Makes the scale decrease by 0.025f 10 times, every 0.05f secounds.
        for (float i = 0f; i <= 1f; i += 0.1f)
        {
            transform.localScale = new Vector2(
            (Mathf.Lerp(transform.localScale.x, transform.localScale.x - 0.025f, Mathf.SmoothStep(0f, 1f, i))),
            (Mathf.Lerp(transform.localScale.y, transform.localScale.y - 0.025f, Mathf.SmoothStep(0f, 1f, i)))
            );
            yield return new WaitForSeconds(0.05f);
        }

        //Coroutine is done, let script know it can be started again.
        coroutineAllowed = true;
    }
}
