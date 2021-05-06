using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{

    private List<GameObject> newInventoryList, oldInventorylist;
    
    private WeaponInventory weaponInventory;

    SpriteRenderer sr;

    void Start()
    {
        weaponInventory = GetComponentInParent<WeaponInventory>();
    }

    
    void Update()
    {
        newInventoryList = weaponInventory.inventoryList;

        if (newInventoryList != oldInventorylist)
        {

        }

        oldInventorylist = newInventoryList;
    }

    private void UpdateDisplay()
    {
        foreach(GameObject weapon in weaponInventory.inventoryList)
        {
            sr = weapon.gameObject.GetComponent<SpriteRenderer>();
            
        }
    }
}
