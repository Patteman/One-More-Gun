using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int nrOfAttacks;
    public int maxNrOfAttacks;

    protected virtual void Start()
    {
        nrOfAttacks = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (nrOfAttacks >= maxNrOfAttacks)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Attack()
    {
        nrOfAttacks++;
    }
}
