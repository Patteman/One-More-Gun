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
    private PlayerUserInterFace inventoryDisplay;

    void Start()
    {
        inventoryList = new List<GameObject>();

        SelectWeapon();

        if (isPlayer)
        {
            inventoryDisplay = this.gameObject.GetComponent<PlayerUserInterFace>();
        }
        else
        {
            foreach (Transform weapon in entityHand.transform)
            {
                inventoryList.Add(weapon.gameObject);
            }
        }

    }

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


        if (Input.GetKeyDown(KeyCode.F))
        {
            DropWeapon();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            EquipWeapon();
        }
    }

    public void CheckInventory()
    {
        if (!isPlayer)
        {
            foreach (Transform weapon in entityHand.transform)
            {
                
                if (weapon.transform.gameObject.activeSelf == true)
                {
                    return;
                }
            }

            inventoryList.Clear();

            foreach (Transform weapon in entityHand.transform)
            {
                inventoryList.Add(weapon.gameObject);
            }
            selectedWeapon = 0;

            try
            {
                entityHand.transform.GetChild(selectedWeapon).gameObject.SetActive(true);
                SelectWeapon();
            }
            catch
            {

            }
        }
        
    }

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
                inventoryDisplay.UpdateDisplay();
                break;
            }
            i++;
        }

        SelectWeapon();
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in entityHand.transform)
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
            weaponYouCanEquip.gameObject.transform.parent = entityHand.transform;
            inventoryList.Add(weaponYouCanEquip);

            SetWeaponPosition(weaponYouCanEquip.transform);
            SelectWeapon();

            floatingText.HideFloatingText();

            if (isPlayer)
                inventoryDisplay.UpdateDisplay();
        }
        catch
        {
            Debug.Log("No Weapon to equip");
        }
    }

    private void SetWeaponPosition(Transform weaponTransform)
    {
        weaponTransform.position = new Vector3(entityHand.transform.position.x, entityHand.transform.position.y, -1);
        weaponTransform.rotation = entityHand.transform.rotation;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == weaponTag && !inventoryList.Contains(col.transform.gameObject) && isPlayer)
        {
            string message = "Press X to equip {0}";
            string weapon = col.transform.name;
            message = string.Format(message, weapon);

            floatingText.ShowFloatingtext(message);
            weaponYouCanEquip = col.gameObject;
        }
        //Debug.Log(col.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == weaponTag && isPlayer)
        {
            floatingText.HideFloatingText();
            weaponYouCanEquip = null;
        }
    }
}