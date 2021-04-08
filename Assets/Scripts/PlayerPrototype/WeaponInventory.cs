using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{

    [Header("Weapons and Inventory")]
    public int inventorySlots;
    public float moveWeaponsBy;
    public GameObject playerHand;
    public GameObject inventoryGameObject;
    private int inventoryIndex;

    private List<GameObject> inventory;
    public List<int> testyListyyInt;
    
    void Start()
    {
        inventory = new List<GameObject>();
        inventory.Clear();
    }

    void Update()
    {
        Update_Inventory();
    }

    private void Update_Inventory()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventoryIndex == 0)
            {                
                inventoryIndex = inventory.Count;
            }
            else
            {
                inventoryIndex -= 1;
            }
            Debug.Log("Q"+inventoryIndex+"##"+inventory.Count);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
           
            if (inventoryIndex == inventory.Count)
            {
                inventoryIndex = 0;
            }
            else
            {
                inventoryIndex += 1;
            }

            Debug.Log("E"+inventoryIndex + "##" + inventory.Count);
        }
        /*
        foreach (GameObject weapon in inventory)
        {
            if (weapon != inventory[inventoryIndex])
            {
                weapon.transform.position = inventoryGameObject.transform.position;
            }
        }*/

        if (Input.GetKeyDown(KeyCode.G))
        {
            PrintTestArr();
            //GetInventoryPositions();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
               
        if (col.transform.tag == "Gun" && !inventory.Contains(col.transform.gameObject))
        {
            if (inventory.Count == 0)
            {
                col.gameObject.transform.parent = playerHand.transform;
                col.gameObject.transform.position = playerHand.transform.position;
                col.gameObject.transform.rotation = playerHand.transform.rotation;                
            }
            else
            {
                col.gameObject.transform.parent =inventoryGameObject.transform;
                col.gameObject.transform.position = inventoryGameObject.transform.position + new Vector3(100*inventory.Count, 0, 0);
            }
            inventory.Add(col.gameObject);

        }
        //PrintTestArr();
       
    }

    private void GetInventoryPositions()
    {
        /*
        foreach(GameObject wep in inventory)
        {
            Debug.Log(wep.transform.position);
        }*/


        for(int i =1; i<= inventory.Count; i++)
        {
            BoxCollider2D rb = inventory[i].GetComponent<BoxCollider2D>();
            //rb.
        }

    }

    private void PrintTestArr()
    {
        string outPut = "";

        foreach (GameObject varInt in inventory)
        {
            outPut = outPut + " " + varInt.name;
        }
        Debug.Log(outPut+" "+inventory.Count);
    }


}
