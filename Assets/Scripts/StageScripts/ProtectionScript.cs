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

        gameObject.AddComponent<BoxCollider2D>();

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    
    void Update()
    {
        
        if (coverHealth <= 0)
        {
            Destroy(gameObject);

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BasicProjectiles")
        {
            coverHealth -= 10f;
        }

        else if (other.gameObject.tag == "RocketProjectiles")
        {
            coverHealth -= 25f;
        }
    }
}
