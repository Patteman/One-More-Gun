using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionScript : MonoBehaviour
{
    public float coverHealth;
    public float maxHealth;


    void Start()
    {
        coverHealth = maxHealth;
    }

    
    void Update()
    {

        if (coverHealth <= 0)
        {
            Destroy(gameObject);
            
        }
    }

    void Collision(Collision2D other)
    {
        if (other.gameObject.tag == "BasicProjectiles")
        {
            coverHealth -= 25;
        }
    }

    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BasicProjectiles")
        {
            coverHealth -= 25f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BasicProjectiles")
        {
            coverHealth -= 25f;
        }
    }
}
