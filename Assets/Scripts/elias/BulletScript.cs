using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfBullet { bullet, rocket, dart }
public class BulletScript : MonoBehaviour
{
    public float speed = 0;
    float lifetime;
    public float maxLifeTime;
    public typeOfBullet type;
    public Rigidbody2D rb;
    public int damageAmount;
    Vector3 direction;
    public GameObject explosionEffect;
    Camera mainCam;

    void Start()
    {
        lifetime = 0f;
        mainCam = Camera.main;
    }

    public void Setup(Vector3 dir)
    {
        this.direction = dir; //the direction in which it moves
    }

    void Update()
    {
        float speed = 20f; 
        rb.velocity = direction * speed;

        Vector3 mouseCameraPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0.0f, 0.0f, angle);

        lifetime += Time.deltaTime;
        if (lifetime >= maxLifeTime)
        {
            Destroy(gameObject); //after a specific time the bullet should be destroyed
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If you hit a target the bullet should realistically not keep travelling.
        if (collision.gameObject.tag == "TARGET")
        {
            if (type == typeOfBullet.rocket)
            {
                GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            //This has been made with the test targets in my stage in mind.
            //In the future this should be replaced with the health of the enemies
            TargetHealthAndStuff tHealth = collision.gameObject.GetComponent<TargetHealthAndStuff>();
            tHealth.health -= damageAmount;
            Destroy(gameObject);
            
        }
    }

}
