using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{

    private List<GameObject> newInventoryList, oldInventorylist;
    private WeaponInventory weaponInventory;


    public GameObject displayObjectGameObject;
    public Canvas canvas;

    public Text maxAmmoText;
    public Text currentAmmoText;

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
            DisplayAmmoCount();
        }
    }

    private void DisplayAmmoCount()
    {
        int selectedWeapon = weaponInventory.selectedWeapon;

        //maxAmmoText.text = weaponInventory.inventoryList[selectedWeapon].

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
