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
    public CameraShake cameraShake;
    private Vector2 screenBounds;

    private float destructionTimer;
    private float objectWidth;
    private float objectHeight;

    public bool onSpawn;
    private bool isMoving;
    private bool madeSound;

    public bool isDead;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
        }
    }

    [Header("Movement")]
    public float movementSpeed;

    public Rigidbody2D rb;

    [Header("Health")]
    public float maxHealth;
    private float currentHealth;
    public Slider hpSlider;
    public Text healthText;

    public GameObject playerHand;

    private Vector2 movement;

    private PlayerUserInterFace playerUserInterFace;

    [Header("Settings")]
    public bool enable60fps;

    void Start()
    {
        onSpawn = true;
        currentHealth = maxHealth;

        isDead = false;
        madeSound = false;
        playerUserInterFace = this.transform.GetComponent<PlayerUserInterFace>();  


        if(enable60fps)
            Application.targetFrameRate = 60;
    }

    private void ManageHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        hpSlider.value = currentHealth;

        healthText.text = currentHealth.ToString();
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
                playerUserInterFace.DisplayAmmoCount();
            }
            catch
            {
                Debug.Log("Weapon script not found");
            }
        }

        if (currentHealth <= 0f)
        {
            destructionTimer += Time.deltaTime;

            if (!madeSound)
            {
                deathAudioSrc.Play();
                madeSound = true;
            }

            if (destructionTimer >= 2.3f)
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
        StartCoroutine(cameraShake.Shake(.15f, .30f));
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
            BulletScript bs = other.gameObject.GetComponent<BulletScript>();
            DecreaseHealth(bs.damageAmount);
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
