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
        fadePercent = lifeTime * 0.7f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

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

    private IEnumerator StartFading()
    {
        coroutineAllowed = false;

        spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.2f);
        yield return new WaitForSeconds(0.2f);

        spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.2f);

        coroutineAllowed = true;
    }
}
