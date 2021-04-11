using System.Collections;
using System;
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

    public GameObject playerHand;

    /*
    [Header("Weapons and Inventory")]
    public int inventorySlots;
    public float moveWeaponsBy;
    
    public GameObject inventoryGameObject;
    private int inventoryIndex;
    private List<GameObject> inventory;
    public int[] invTestArr;

    */

    //Private
    private Vector2 movement;


    public Component[] gs;

    void Start()
    {
        //inventory = new List<GameObject>();
        //inventoryIndex = inventory.Count;
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
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //gs = gameObject.GetComponentsInChildren<GunScript>();
            Debug.Log("Space pressed");
            Debug.Log(playerHand.name);
            GunScript daGunScript = playerHand.GetComponentInChildren<GunScript>();
           
            if(daGunScript != null)
            {
                daGunScript.Shoot();
                Debug.Log("Shoot");
            }
            else if(daGunScript == null)
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
}
