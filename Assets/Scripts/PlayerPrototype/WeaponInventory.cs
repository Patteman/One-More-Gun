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

    public void CycleInventoryRight()
    {

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
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.E))
        {
            CycleInventoryRight();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleInventoryLeft();
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            DropWeapon();
        }

        if (inventoryList.Count != entityHand.transform.childCount)
        {
            CycleInventoryRight();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            EquipWeapon();
        }
    }

    public void UpdateInventoryList()
    {
        inventoryList.Clear();

        foreach (Transform weapon in entityHand.transform)
        {
            inventoryList.Add(weapon.gameObject);
        }

        SelectWeapon();
    }

    private void DropWeapon()
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

    private void SelectWeapon()
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
            Debug.Log("Equipped weapon");
            if (isPlayer)
                playerUserInterface.UpdateDisplay();
        }
        catch
        {
            Debug.Log("No Weapon to equip");
        }
    }

    private void SetWeaponPosition(Transform weaponTransform)
    {
        weaponTransform.position = new Vector3(entityHand.transform.position.x, entityHand.transform.position.y, entityHand.transform.position.z);
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
        Debug.Log(col.gameObject.name);
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