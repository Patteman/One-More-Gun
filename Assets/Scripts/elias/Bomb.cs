using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{
    public GameObject objectToDropOrThrow;
    Explosive eScript;

    //int maxAmount;
    //int currentAmount;

    protected override void Start()
    {
        base.Start();
        //currentAmount = 0;
        //maxAmount=5;
        //This script reference is needed because it contains a bool which determines if the bomb has been activated or not.
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ////Click left mouse button to drop a bomb. The amount of bombs you've used will go up by one.
        //if (Input.GetMouseButtonDown(0))
        //{
        //    DropObject(objectToDropOrThrow);
        //    currentAmount++;
        //}
        ////If the maximum amount has passed, this weapon will (should) be removed from your inventory.
        //if (currentAmount >= maxAmount)
        //{
        //    Destroy(gameObject);
        //}
    }
    public override void Attack()
    {
        base.Attack();
        GameObject explosive = Instantiate(objectToDropOrThrow, transform.position, Quaternion.identity);
        eScript = explosive.GetComponent<Explosive>();
        eScript.setOff = true;
    }

    //void DropObject(GameObject gameobj)
    //{
    //    //puts out a bomb which gets activated.
    //    GameObject explosive = Instantiate(objectToDropOrThrow, transform.position, Quaternion.identity);
    //    eScript = explosive.GetComponent<Explosive>();
    //    eScript.setOff = true;
    //}
}
