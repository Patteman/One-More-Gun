using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealthAndStuff : MonoBehaviour
{
    public int health;
    public bool poisoned;
    float poisonInterval;
    float poisonTimer;

    void Start()
    {
        poisonInterval = 5;
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
        poisonInterval += Time.deltaTime;
        
        if (poisonInterval >= 0.3)
        {
            poisonInterval = 0;
            health--;
        }
        poisonTimer+=Time.deltaTime;
        if (poisonTimer >= 10)
        {
            poisonTimer = 0;
            poisoned = false;
        }
    }
    private void OnParticleTrigger()
    {
        health -= 50;
    }

}//Thanks to David Täljsten for helping me figure out the health drainage system
