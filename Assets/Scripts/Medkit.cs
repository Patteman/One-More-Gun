using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public int healAmount = 30;
    public float lifeTime = 10f;
    
    private float fadePercent;
    private bool coroutineAllowed;
    private float despawnTimer;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        coroutineAllowed = true;
        despawnTimer = 0f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //Sets fadePercent to 70% of the lifeTime value.
        fadePercent = lifeTime * 0.7f;
    }

    //Start counting, check if count is above fade% and if the coroutine is started or not, then start coroutine.
    void Update()
    {
        despawnTimer += Time.deltaTime;
        if (despawnTimer >= fadePercent && coroutineAllowed)
        {
            StartCoroutine("StartFading");
            if (despawnTimer >= lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }

    //Makes the medkit "blink" every 0.2 seconds, to signal that the object will soon dissapear.
    private IEnumerator StartFading()
    {
        //Makes sure the coroutine cannot be started again before the code has been run fully.
        coroutineAllowed = false;

        //Sets the Alpha of the medkit to 0.2f (semi-transparent) then waits 0.2 seconds.
        spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.2f);
        yield return new WaitForSeconds(0.2f);

        //Sets the Alpha of the medkit to 1f (full color) then waits 0.2 seconds.
        spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.2f);

        //Coroutine is done, let script know it can be started again.
        coroutineAllowed = true;
    }
}
