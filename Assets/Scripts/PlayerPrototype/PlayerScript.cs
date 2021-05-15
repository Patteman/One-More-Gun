using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public AudioSource playerAudioSrc;
    public AudioSource deathAudioSrc;

    public Camera MainCamera;
    private Vector2 screenBounds;

    private float destructionTimer;
    private float objectWidth;
    private float objectHeight;

    //public float movementSpeed;

    public bool onSpawn;
    private bool isMoving;
    private bool madeSound;

    //A classic
    private bool isDead;

    [Header("Movement")]
    public float movementSpeed;

    public Rigidbody2D rb;

    [Header("Health")]
    public float maxHealth;
    private float currentHealth;
    public Slider hpSlider;
    public Text testText;

    public GameObject playerHand;

    private Vector2 movement;

    void Start()
    {
        onSpawn = true;
        currentHealth = maxHealth;
        isDead = false;
        madeSound = false;
    }

    private void ManageHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        hpSlider.value = currentHealth;

        testText.text = currentHealth.ToString();
    }
    
    void Update()
    {
        if (isDead)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        ManageHealth();
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Weapon weaponScript = playerHand.GetComponentInChildren<Weapon>();

            try
            {
                weaponScript.Attack();
            }
            catch
            {
                Debug.Log("Weapon script not found");
            }
        }

        if (currentHealth <= 0f)
        {
            destructionTimer += Time.fixedDeltaTime;

            if (!madeSound)
            {
                deathAudioSrc.Play();
                madeSound = true;
            }

            if (destructionTimer >= 2.5f)
            {
                Die();
            }
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
        }

        else if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
        {
            isMoving = false;
        }
    }

    private void IncreaseHealth()
    {
        currentHealth += 10;
    }

    private void DecreaseHealth(float damageTaken)
    {
        currentHealth -= damageTaken;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        if (!isMoving)
        {
            playerAudioSrc.Stop();
        }

        if (isMoving && playerAudioSrc.isPlaying == false)
        {
            playerAudioSrc.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BasicProjectiles")
        {
            DecreaseHealth(50);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("You've reached the goal!");
            SceneManager.LoadScene("WinMenu");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            Debug.Log("You've left the spawn!");
            onSpawn = false;
        }
    }

    private void Die()
    {
        isDead = true;
        SceneManager.LoadScene("LoseMenu");
        Destroy(gameObject);
    }
}
