using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Here it is, the base class that is referenced to in most weapon scripts! So, what does it actually do?

public class Weapon : MonoBehaviour
{
    public int nrOfAttacks;
    public int maxNrOfAttacks;

    protected virtual void Start()
    {
        //The number of bullets, bombs layed out, hits with melee weapons, etc.
        //Set to 0 to begin with.
        nrOfAttacks = 0;
    }
    
    protected virtual void Update()
    {       
        //There is a maximum amount of "attacks" one can do with a weapon.
        //If the current number is equal to or larger than that number, the weapon disappears.

        if (nrOfAttacks >= maxNrOfAttacks)
        {
            Destroy(gameObject);
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Attack();
        //}
    }

    public virtual void Attack()
    {
        //Increases the current number of attacks each time you attack.
        nrOfAttacks++;
    }
}
