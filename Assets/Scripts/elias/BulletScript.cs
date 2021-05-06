using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfBullet { bullet, rocket, dart }
enum BulletOrigin { enemy, player }
public class BulletScript : MonoBehaviour
{
    EnemyAgentTest enemy;
    public float speed = 0;

    float lifetime;
    public float maxLifeTime;
    public int damageAmount;

    public TypeOfBullet type;
    public Rigidbody2D rb;
    BulletOrigin firedBy;

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
        //Vector3 mouseCameraPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0.0f, 0.0f, angle);
    }

    void Update()
    {
        float speed = 10f;
        rb.velocity = direction * speed;
        lifetime += Time.deltaTime;
        if (lifetime >= maxLifeTime)
        {
            Destroy(gameObject); //after a specific time the bullet should be destroyed
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
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "TARGET")
        {
            //This has been made with the test targets in my stage in mind.
            //In the future this should be replaced with the health of the enemies
            TargetHealthAndStuff tHealth = collision.gameObject.GetComponent<TargetHealthAndStuff>();
            tHealth.health -= damageAmount;
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
