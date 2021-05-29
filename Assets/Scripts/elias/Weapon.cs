using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Here it is, the base class that is referenced to in most weapon scripts! So, what does it actually do?

public class Weapon : MonoBehaviour
{
    public int nrOfAttacks;
    public int maxNrOfAttacks;

    private float destructionTimer;

    public AudioSource outOfAttacksSoundSrc;

    protected virtual void Start()
    {
        // The number of bullets, bombs layed out, hits with melee weapons, etc.
        // Set to 0 to begin with.
        nrOfAttacks = 0;
    }
    
    protected virtual void Update()
    {       
        // There is a maximum amount of "attacks" one can do with a weapon.
        // If the current number is equal to or larger than that number, the weapon disappears.

        if (nrOfAttacks >= maxNrOfAttacks)
        {
            destructionTimer += Time.fixedDeltaTime;
            outOfAttacksSoundSrc.Play();

            if (destructionTimer >= 0.2f)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual void Attack()
    {
        // Increases the current number of attacks each time you attack.
        nrOfAttacks++;
    }

    // Enemy characters use this method. The differences will be in the separate scripts for weapons.
    public virtual void EnemyAttack(Transform target)
    {
        nrOfAttacks++;
    }
}
