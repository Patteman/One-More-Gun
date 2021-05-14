using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{

    private List<GameObject> newInventoryList, oldInventorylist;
    private WeaponInventory weaponInventory;


    public GameObject spriteGO;
    public Canvas canvas;

    public float distance;
    private float spritePos;

    private SpriteRenderer sr;




    void Start()
    {
        weaponInventory = this.transform.GetComponent<WeaponInventory>();
    }
    /* GameObject enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
 enemy.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
 
         
         */

    void Update()
    {


        newInventoryList = weaponInventory.inventoryList;
        UpdateDisplay();
        //Debug.Log(weaponInventory.inventoryList.Count + "||" + newInventoryList.Count + "||" + oldInventorylist.Count);
        oldInventorylist =weaponInventory.inventoryList;

        //Debug.Log(weaponInventory.inventoryList.Count+"||"+newInventoryList.Count+"||"+oldInventorylist.Count);

    }

    private void UpdateDisplay()
    {
        if (oldInventorylist != weaponInventory.inventoryList)
        {
           
            spritePos = 0;

            foreach(Transform sprite in canvas.transform)
            {
                if(sprite.tag=="UI_Inventory")
                    GameObject.Destroy(sprite.gameObject);
            }

            foreach(GameObject weapon in weaponInventory.inventoryList)
            {
                spritePos += distance;
                Debug.Log("Dabadapda");
                GameObject var = Instantiate(spriteGO, new Vector3(0, 0, 0), Quaternion.identity);

                var.transform.parent = canvas.transform;

                var.gameObject.transform.localPosition = new Vector3(spritePos, 0, 0);

                sr = weapon.gameObject.GetComponent<SpriteRenderer>();

                Sprite sprite = spriteGO.GetComponent<Sprite>();

                sprite = sr.sprite;
            }

        }

    }
}
