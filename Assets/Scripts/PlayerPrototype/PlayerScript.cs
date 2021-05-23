using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public bool onSpawn;

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
    public Text testText;

    public GameObject playerHand;

    private Vector2 movement;

    private PlayerUserInterFace playerUserInterFace;

    [Header("Settings")]
    public bool enable60fps;

    void Start()
    {
        onSpawn = true;
        currentHealth = maxHealth;

        playerUserInterFace = this.transform.GetComponent<PlayerUserInterFace>();


        if(enable60fps)
            Application.targetFrameRate = 60;

    }

    private void ManageHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        hpSlider.value = currentHealth;

        testText.text = currentHealth.ToString();
    }
    
    void Update()
    {        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        ManageHealth();
        
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Weapon gunScript = playerHand.GetComponentInChildren<Weapon>();

            try
            {
                gunScript.Attack();
                playerUserInterFace.DisplayAmmoCount();
            }
            catch
            {
                Debug.Log("Gun script not found");
            }
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("You've reached the goal!");
        }
        
        if (other.gameObject.tag == "BasicProjectiles")
        {
            Debug.Log(other.gameObject.name);
            DecreaseHealth(5);
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
}
