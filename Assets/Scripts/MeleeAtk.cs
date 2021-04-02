﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtk : MonoBehaviour
{
    bool isAttacking;
    public int damageAmount;
    int numberOfAttacks;
    public int maxAttackAmount;

    void Start()
    {
        isAttacking = false;
        numberOfAttacks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            numberOfAttacks++;
            isAttacking = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (numberOfAttacks >= maxAttackAmount)
        {
            Destroy(gameObject);
        }
    }
}
