using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    [Header("Check this if the object is the player")]
    public bool isPlayer;

    [Header("Weapons and Inventory")]
    public GameObject entityHand;

    public string weaponTag;

    public List<GameObject> inventoryList;

    public int selectedWeapon = 0;

    public FloatingText floatingText;

    private GameObject weaponYouCanEquip;

    private PlayerUserInterFace playerUserInterface;

    void Start()
    {
        inventoryList = new List<GameObject>();

        SelectWeapon();

        if (isPlayer)
        {
            playerUserInterface = this.gameObject.GetComponent<PlayerUserInterFace>();
        }
        else
        {
            foreach (Transform weapon in entityHand.transform)
            {
                inventoryList.Add(weapon.gameObject);
            }
        }

    }

    //Cycles to the "right" weapon in inventory
    public void CycleInventoryRight()
    {
        Debug.Log("Right");
        if (selectedWeapon >= entityHand.transform.childCount - 1)
        {
            selectedWeapon = 0;
        }
        else
        {
            selectedWeapon++;
        }
    }

    //Cycles to the "left" weapon in inventory
    public void CycleInventoryLeft()
    {
        Debug.Log("Left");
        if (selectedWeapon <= 0)
        {
            selectedWeapon = entityHand.transform.childCount - 1;
        }
        else
        {
            selectedWeapon--;
        }
    }


    void Update()
    {
        if (!isPlayer)
        {
            CheckInventory();
            return;
        }

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.E))
        {
            CycleInventoryRight();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleInventoryLeft();
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        
        if (Input.GetKeyDown(KeyCode.X))
        {
            DropWeapon();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            EquipWeapon();
        }
    }

    //Used by enemy to determine if they have a weapon in their inventory and if they have one equipped and if not, equip one

    public void CheckInventory()
    {
        if (!isPlayer)
        {
            foreach (Transform weapon in entityHand.transform)
            {
                //Determines if the enemy has a weapon equiped
                if (weapon.transform.gameObject.activeSelf == true)
                {
                    return;
                }
            }

            //Resets inventory
            inventoryList.Clear();

            foreach (Transform weapon in entityHand.transform)
            {
                inventoryList.Add(weapon.gameObject);
            }
            selectedWeapon = 0;

            //enables a weapon
            try
            {
                entityHand.transform.GetChild(selectedWeapon).gameObject.SetActive(true);
                SelectWeapon();
            }
            catch
            {
                Debug.Log("Weapon already equiped");
            }
        }
        
    }

    //Cycles through the weapons an deparents the active weapon
    public void DropWeapon()
    {
        int i = 0;
        foreach (Transform weapon in entityHand.transform)
        {
            if (weapon.gameObject.activeSelf == false)
            {
                selectedWeapon = i;
            }
            else if (weapon.gameObject.activeSelf == true)
            {
                weapon.parent = null;
                weapon.position = entityHand.transform.position;
                inventoryList.Remove(weapon.gameObject);

                try
                {
                    playerUserInterface.UpdateDisplay();
                }
                catch
                {
                    Debug.Log("Not a player character");
                }
                

                break;
            }
            i++;
        }

        SelectWeapon();
    }

    //Sets the weapon matching the SelectedWeapon int to active
    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in entityHand.transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                SetWeaponPosition(weapon);
                try
                {
                    playerUserInterface.UpdateDisplay();
                }
                catch
                {
                    Debug.Log("Not the player");
                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
        
    }

    //Adds the weaponYouCanEquip to the inventory list and sets it parent to the entity hand
    private void EquipWeapon()
    {
        try
        {

            weaponYouCanEquip.gameObject.transform.SetParent(entityHand.transform);
            inventoryList.Add(weaponYouCanEquip);

            SetWeaponPosition(weaponYouCanEquip.transform);
            SelectWeapon();

            floatingText.HideFloatingText();

            if (isPlayer)
                playerUserInterface.UpdateDisplay();
        }
        catch
        {
            Debug.Log("No Weapon to equip");
        }
    }

    //Sets to rotation and position of the weapon so it matches the entity hand
    private void SetWeaponPosition(Transform weaponTransform)
    {
        weaponTransform.position = new Vector3(entityHand.transform.position.x, entityHand.transform.position.y, -1);
        weaponTransform.rotation = entityHand.transform.rotation;
    }

    //Checks if the collider is a weapon that the player an pick up, if so, sets the weapon to weaponYouCanEquip
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == weaponTag && !inventoryList.Contains(col.transform.gameObject) && isPlayer)
        {
            string message = "Press F to equip {0}";
            string weapon = col.transform.name;
            message = string.Format(message, weapon);

            floatingText.ShowFloatingtext(message);
            weaponYouCanEquip = col.gameObject;
        }
    }

    //Nulls the weaponYouCanEquip when collision ends
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == weaponTag && isPlayer)
        {
            floatingText.HideFloatingText();
            weaponYouCanEquip = null;
        }
    }
}