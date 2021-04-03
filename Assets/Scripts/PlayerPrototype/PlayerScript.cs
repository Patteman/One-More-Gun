using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public Rigidbody2D rb;

    [Header("Health")]
    public float maxHealth;
    private float currentHealth;
    public Slider hpSlider;
    public Text testText;

    //Private
    private Vector2 movement;


    public Component[] gs;

    void Start()
    {
        
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
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        ManageHealth();

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            IncreaseHealth();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            DecreaseHealth();
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gs = gameObject.GetComponentsInChildren<GunScript>();

           

            GunScript daGunScript = GetComponentInChildren<GunScript>();

            if(daGunScript != null)
            {
                Debug.Log("YEEEEEEEEEEEEEEEEEE");
                daGunScript.Shoot();
            }
            else if(daGunScript == null)
            {
                Debug.Log("Ah, piss");
            }
            
        }

    }

    private void IncreaseHealth()
    {
        currentHealth += 10;
    }

    private void DecreaseHealth()
    {
        currentHealth -= 10;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
