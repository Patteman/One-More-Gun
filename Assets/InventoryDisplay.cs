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
    
    public float distance;
    private float spritePos;

    void Start()
    {
        weaponInventory = this.transform.GetComponent<WeaponInventory>();
        
    }

    void Update()
    {
        newInventoryList = weaponInventory.inventoryList;

        oldInventorylist =weaponInventory.inventoryList;

    }

    public void UpdateDisplay()
    {
            spritePos = distance;

            foreach (Transform sprite in canvas.transform)
            {
                if(sprite.tag=="UI_Inventory")
                    GameObject.Destroy(sprite.gameObject);
            }

            foreach(GameObject weapon in weaponInventory.inventoryList)
            {
                spritePos += distance*(-1);
                
                GameObject var = Instantiate(displayObjectGameObject, new Vector3(0, 0, 0), Quaternion.identity);

                var.transform.parent = canvas.transform;

                var.gameObject.transform.localPosition = new Vector3(spritePos, -5, 0);

                SpriteRenderer weaponSR = weapon.gameObject.GetComponent<SpriteRenderer>();

                Image displayObjectSR = var.GetComponent<Image>();

                displayObjectSR.sprite = weaponSR.sprite;
            }
    }
}
