using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed;
    public float maxHealth = 100;
    public float health;
    public static bool isDead;

    private void Start()
    {
        speed = 5f;
        health = maxHealth;
        isDead = false;
    }

    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BasicProjectiles")
        {
            TakeDamage(25f);
        }
    }

    void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            Die();
        }

        Debug.Log("Damage taken");
    }

    void Die()
    {
        Destroy(gameObject); //Destroy play atm, change to more (animation/sfx/change to lose screen etc)
        isDead = true;
    }
}
