using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfBullet { bullet, rocket, dart }
public class BulletScript : MonoBehaviour
{
    public float speed;
    float lifetime;
    public float maxLifeTime;
    public typeOfBullet type;
    public Rigidbody2D rb;
    public int damageAmount;
    Vector3 direction;
    public GameObject explosionEffect;

    void Start()
    {
        lifetime = 0f;
    }

    public void Setup(Vector3 dir)
    {
        //the direction in which the object moves. Used in scripts that instantiate a bullet.
        this.direction = dir;

        //Rotates the object to face the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0.0f, 0.0f, angle);
    }

    void Update()
    {
        //Sets velocity of the rigidbody
        rb.velocity = direction * speed;

        //Adds to the object's "lifetime".
        lifetime += Time.deltaTime;

        //If lifetime is more than or equal to the maximum lifetime, the object is to be destroyed.
        if (lifetime >= maxLifeTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TARGET")
        {
            //Reduces health if hit target is an "enemy"
            //This has been made with the test targets in my stage in mind.
            //In the future this should be replaced with the health of the enemies
            TargetHealthAndStuff tHealth = collision.gameObject.GetComponent<TargetHealthAndStuff>();
            tHealth.health -= damageAmount;
            if (type == typeOfBullet.rocket)
            {
                GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

}
