using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUserInterFace : MonoBehaviour
{

    private List<GameObject> newInventoryList, oldInventorylist;
    private WeaponInventory weaponInventory;


    public GameObject displayObjectGameObject;
    public GameObject inventoryDisplayParent;

    public Text ammoText;
        
    public float distance;
    private float spritePos;

    private Weapon weapon;

    void Start()
    {
        weaponInventory = this.transform.GetComponent<WeaponInventory>();
    }

    void Update()
    {
        //Checks if the player has any weapons eqiped to determine if it should show ammo counter
        if (weaponInventory.inventoryList.Count >= 1 && weaponInventory.inventoryList.Count != 0)
        {
            ammoText.gameObject.SetActive(true);
            DisplayAmmoCount();
        }
        else if(weaponInventory.inventoryList.Count == 0)
        {
            ammoText.gameObject.SetActive(false);
        }
    }

    //Aquires current and max ammo of equiped weapon and shows it on screen
    public void DisplayAmmoCount()
    {
        int selectedWeapon = weaponInventory.selectedWeapon;
        try
        {
            weapon = weaponInventory.inventoryList[selectedWeapon].GetComponent<Weapon>();

            string ammoCounterMessage = "{0}/{1}";

            string maxAmmo = weapon.maxNrOfAttacks.ToString();

            string currentAmmo = (weapon.maxNrOfAttacks - weapon.nrOfAttacks).ToString();

            ammoCounterMessage = string.Format(ammoCounterMessage, currentAmmo, maxAmmo);

            ammoText.text = ammoCounterMessage;
        }
        catch
        {

        }
    }

    //Shows inventory on screen
    public void UpdateDisplay()
    {
        spritePos = distance;

        //clears current images on screen
        foreach (Transform sprite in inventoryDisplayParent.transform)
        {
            if (sprite.tag == "UI_Inventory")
            {
                GameObject.Destroy(sprite.gameObject);
            }
                
        }

        int i = 0;
        
        //loops through inventory and adds it sprite to the screen
        foreach (GameObject weapon in weaponInventory.inventoryList)
        {
            GameObject weaponImage = Instantiate(displayObjectGameObject, new Vector3(0, 0, 0), Quaternion.identity);

            weaponImage.transform.SetParent(inventoryDisplayParent.transform);

            weaponImage.gameObject.transform.localPosition = new Vector3(distance * i, 0, -1);

            SpriteRenderer weaponSR = weapon.gameObject.GetComponent<SpriteRenderer>();

            Image displayObjectSR = weaponImage.GetComponent<Image>();

            displayObjectSR.sprite = weaponSR.sprite;

            if (i == weaponInventory.selectedWeapon)
            {
                displayObjectSR.rectTransform.localScale = new Vector3(displayObjectSR.rectTransform.localScale.x+0.5f, displayObjectSR.rectTransform.localScale.y + 0.5f, displayObjectSR.rectTransform.localScale.z + 0.5f);
            }

            i++;
        }

    }
}
