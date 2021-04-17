using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{
    public GameObject objectToDropOrThrow;
    Explosive eScript;

    protected override void Start()
    {
        //Inherits start method from Weapon.cs
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //Inherits update method from Weapon.cs
        base.Update();
    }
    public override void Attack()
    {
        //Inherits Attack method from Weapon.cs
        base.Attack();

        //Instantiates an explosive with the Explosive script, and sets the explosive off.
        GameObject explosive = Instantiate(objectToDropOrThrow, transform.position, Quaternion.identity);
        eScript = explosive.GetComponent<Explosive>();
        eScript.setOff = true;
    }
}
