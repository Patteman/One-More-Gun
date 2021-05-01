using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{

    [Header("Weapons and Inventory")]
    public GameObject playerHand, dropLocation;

    public List<GameObject> inventoryList;

    public int selectedWeapon = 0;

    public FloatingText floatingText;

    private GameObject weaponYouCanEquip;

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

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DropWeapon();
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            EquipWeapon();
        }
    }

    private void DropWeapon()
    {
        int i = 0;
        foreach (Transform weapon in playerHand.transform)
        {
            if (weapon.gameObject.activeSelf == false)
            {
                selectedWeapon = i;
            }
            else if (weapon.gameObject.activeSelf == true)
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
        foreach (Transform weapon in playerHand.transform)
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

    private void EquipWeapon()
    {
        try
        {
            weaponYouCanEquip.gameObject.transform.parent = playerHand.transform;
            inventoryList.Add(weaponYouCanEquip);

            SetWeaponPosition(weaponYouCanEquip.transform);
            SelectWeapon();
        }
        catch
        {
            Debug.Log("No Weapon to equip");
        }
    }

    private void SetWeaponPosition(Transform weaponTransform)
    {
        weaponTransform.position = new Vector3(playerHand.transform.position.x, playerHand.transform.position.y, playerHand.transform.position.z);
        weaponTransform.rotation = playerHand.transform.rotation;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Gun" && !inventoryList.Contains(col.transform.gameObject))
        {
            string message = "Press X to equip {0}";
            string weapon = col.transform.name;
            message = string.Format(message, weapon);

            floatingText.ShowFloatingtext(message);
            weaponYouCanEquip = col.gameObject;
        }
        Debug.Log(col.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Gun")
        {
            floatingText.HideFloatingText();
            weaponYouCanEquip = null;
        }
    }
}
