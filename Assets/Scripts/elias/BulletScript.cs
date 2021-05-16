using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfBullet { bullet, rocket, dart }
public class BulletScript : MonoBehaviour
{
    EnemyAgentTest enemy;
    public float speed = 0;

    EnemyAgentTest enemyScript;

    float lifetime;
    public float maxLifeTime;
    public int damageAmount;

    public TypeOfBullet type;
    public Rigidbody2D rb;

    public GameObject explosionEffect;

    Vector3 direction;
    Camera mainCam;

    void Start()
    {
        lifetime = 0f;
    }

    public void Setup(Vector3 dir)
    {
        this.direction = dir;
        mainCam = Camera.main;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0.0f, 0.0f, angle);
    }

    void Update()
    {
        rb.velocity = direction * speed;

        lifetime += Time.deltaTime;
        if (lifetime >= maxLifeTime)
        {
            //after a specific time the bullet should be destroyed
            Destroy(gameObject); 
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemies")
        {
            collision.gameObject.SendMessage("TakeDamage", damageAmount); 
            Destroy(gameObject);
        }

        if (type == TypeOfBullet.rocket)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "BasicProtection")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }

    }

}
