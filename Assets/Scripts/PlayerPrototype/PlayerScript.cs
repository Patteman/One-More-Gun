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

    //public float movementSpeed;

    public bool onSpawn;


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

        CalculateScreenSize();
        onSpawn = true;

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

    private void LateUpdate()
    {
        UpdateBounds();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            Debug.Log("You've left the spawn!");
            onSpawn = false;
        }
    }

    private void CalculateScreenSize()
    {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    private void UpdateBounds()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
