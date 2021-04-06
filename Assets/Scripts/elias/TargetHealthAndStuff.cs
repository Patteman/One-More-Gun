using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealthAndStuff : MonoBehaviour
{
    public int health;
    public bool poisoned;
    float poisonTimer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (poisoned == true)
        {
            HealthDrain();
        }
        if (health <= 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "POISONOUS")
        {
            poisoned = true;
        }
    }
    private void HealthDrain()
    {
        poisonTimer += Time.deltaTime;
        StartCoroutine(DrainHealth());
        if (poisonTimer >= 5)
        {
            poisonTimer = 0;
            poisoned = false;
        }
        Debug.Log(Time.deltaTime);
    }

    IEnumerator DrainHealth()
    {
        health -= 1;
        yield return new WaitForSeconds(3f);
    }
}
