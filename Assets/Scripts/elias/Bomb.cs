using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject objectToDropOrThrow;
    Explosive eScript;

    int maxAmount;
    int currentAmount;

    void Start()
    {
        currentAmount = 0;
        maxAmount=5;
        //This script reference is needed because it contains a bool which determines if the bomb has been activated or not.
        eScript = objectToDropOrThrow.GetComponent<Explosive>();
    }

    // Update is called once per frame
    void Update()
    {
        //Click left mouse button to drop a bomb. The amount of bombs you've used will go up by one.
        if (Input.GetMouseButtonDown(0))
        {
            DropObject(objectToDropOrThrow);
            currentAmount++;
        }
        //If the maximum amount has passed, this weapon will (should) be removed from your inventory.
        if (currentAmount >= maxAmount)
        {
            Destroy(gameObject);
        }
    }
    void DropObject(GameObject gameobj)
    {
        //puts out a bomb which gets activated.
        GameObject explosive = Instantiate(objectToDropOrThrow, transform.position, Quaternion.identity);
        eScript.setOff = true;
    }
}
