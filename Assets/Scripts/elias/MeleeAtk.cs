using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I am very unsure on what to do here. I just have a simple thing that flips the "knife" and says to attack. 
// I think ideally this should be done with animation and colliders in final product.
public class MeleeAtk : Weapon
{
    bool isAttacking;
    public int damageAmount;
    //int numberOfAttacks;
    //public int maxAttackAmount;

    //void Start()
    //{
    //    isAttacking = false;
    //    numberOfAttacks = 0;
    //}
    protected override void Start()
    {
        base.Start();
        isAttacking = false;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonUp(0))
            isAttacking = false;

        //In the future this should be replaced by an animation,
        //or a more appropriate visual representation of the player character using the sword or knife to attack an enemy.
        if (isAttacking == false)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (isAttacking == true)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public override void Attack()
    {
        base.Attack();
        isAttacking = true;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        numberOfAttacks++;
    //        isAttacking = true;
    //        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    //    }
    //    else if (Input.GetMouseButtonUp(0))
    //    {
    //        isAttacking = false;
    //        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    //    }
    //    if (numberOfAttacks >= maxAttackAmount)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
