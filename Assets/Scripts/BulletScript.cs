using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfBullet { bullet, rocket }
public class BulletScript : MonoBehaviour
{
    public float speed=0;
    float lifetime;
    public float maxLifeTime;
    public typeOfBullet type;
    public Rigidbody2D rb;
    Vector3 direction;
    public GameObject explosionEffect;

    void Start()
    {
        lifetime = 0f;       //Changed by Kevin
        maxLifeTime = 1f;    //Changed by Kevin
    }
    
    public void Setup(Vector3 dir)
    {
        this.direction = dir; //the direction in which it moves
    }

    void Update()
    {
        float speed = 10f; //FIX     //Changed by Kevin to test
        //transform.position += direction * speed * Time.deltaTime; //the position changes
        rb.velocity = direction * speed;
        lifetime+=Time.deltaTime;
        if (lifetime >= maxLifeTime)
        {
            Destroy(gameObject); //after a specific time the bullet should be destroyed
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TARGET")
        {
            if (type == typeOfBullet.rocket)
            {
                GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
                
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Walls")
        {
            Destroy(gameObject); // <- For now. Projectile / Timer until it's destroyed. -> Ricochet-effect or something like that.
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "BasicProtection")
        {
            Destroy(gameObject);
        }
    }

}
