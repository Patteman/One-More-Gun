using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{

    private List<GameObject> newInventoryList, oldInventorylist;
    private WeaponInventory weaponInventory;
    void Start()
    {
        weaponInventory = GetComponentInParent<WeaponInventory>();
    }

    
    void Update()
    {

        newInventoryList = weaponInventory.inventoryList;
    }
}
