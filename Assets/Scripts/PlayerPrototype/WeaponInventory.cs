using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{

    [Header("Weapons and Inventory")]
    public GameObject playerHand, dropLocation;

    private List<GameObject> inventoryList;

    public int selectedWeapon = 0;
    
    void Start()
    {
        inventoryList = new List<GameObject>();

        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedWeapon >= playerHand.transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = playerHand.transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if(previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DropWeapon();
        }

    }

    private void DropWeapon()
    {
        int i = 0;
        foreach (Transform weapon in playerHand.transform)
        {
            if(weapon.gameObject.active == false)
            {
                selectedWeapon = i;
            }
            else if(weapon.gameObject.active == true)
            {
                weapon.parent = null;
                weapon.position = dropLocation.transform.position;
                inventoryList.Remove(weapon.gameObject);
                break;
            }
            i++;
        }

        SelectWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in playerHand.transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                SetWeaponPosition(weapon);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }

    private void SetWeaponPosition(Transform weaponTransform)
    {
        weaponTransform.position = new Vector3(playerHand.transform.position.x , playerHand.transform.position.y , playerHand.transform.position.z);
        weaponTransform.rotation = playerHand.transform.rotation;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
               
        if (col.transform.tag == "Gun" && !inventoryList.Contains(col.transform.gameObject))
        {
            col.gameObject.transform.parent = playerHand.transform;
            col.gameObject.transform.position = playerHand.transform.position;
            col.gameObject.transform.rotation = playerHand.transform.rotation;                
          
            inventoryList.Add(col.gameObject);

            SetWeaponPosition(col.transform);
            SelectWeapon();
        }

        Debug.Log(col.gameObject.name);
    }    
}
