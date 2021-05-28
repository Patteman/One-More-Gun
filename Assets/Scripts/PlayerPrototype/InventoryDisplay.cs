using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay: MonoBehaviour
{
    [Header("Please dont use this, use PlayerUserInterFace instead")]
    private List<GameObject> newInventoryList, oldInventorylist;
    private WeaponInventory weaponInventory;


    public GameObject displayObjectGameObject;
    public Canvas canvas;

    public Text ammoText;

    public Vector3 displayObjectStartPos;
    public float distance;
    private float spritePos;

    private Weapon weapon;

    void Start()
    {
        weaponInventory = this.transform.GetComponent<WeaponInventory>();
    }

    void Update()
    {
        if (weaponInventory.inventoryList.Count >= 1)
        {
            //DisplayAmmoCount();
        }
    }

    public void DisplayAmmoCount()
    {
        int selectedWeapon = weaponInventory.selectedWeapon;

        weapon = weaponInventory.inventoryList[selectedWeapon].GetComponent<Weapon>();

        string ammoCounterMessage = "{0}/{1}";

        string maxAmmo = weapon.maxNrOfAttacks.ToString();

        string currentAmmo = (weapon.maxNrOfAttacks - weapon.nrOfAttacks).ToString();

        ammoCounterMessage = string.Format(ammoCounterMessage, currentAmmo, maxAmmo);

        ammoText.text = ammoCounterMessage;
    }

    public void UpdateDisplay()
    {
        spritePos = distance;

        foreach (Transform sprite in canvas.transform)
        {
            if (sprite.tag == "UI_Inventory")
                GameObject.Destroy(sprite.gameObject);
        }

        int i = 0;

        foreach (GameObject weapon in weaponInventory.inventoryList)
        {
            GameObject var = Instantiate(displayObjectGameObject, new Vector3(0, 0, 0), Quaternion.identity);

            var.transform.parent = canvas.transform;

            var.gameObject.transform.localPosition = new Vector3(displayObjectStartPos.x + distance * i, displayObjectStartPos.y, displayObjectStartPos.z);

            SpriteRenderer weaponSR = weapon.gameObject.GetComponent<SpriteRenderer>();

            Image displayObjectSR = var.GetComponent<Image>();

            displayObjectSR.sprite = weaponSR.sprite;

            i++;
        }

    }
}
