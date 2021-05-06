using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I am very unsure on what to do here. I just have a simple thing that flips the "knife" and says to attack. 
// I think ideally this should be done with animation and colliders in final product.
public class MeleeAtk : Weapon
{
    bool isAttacking;
    public int damageAmount;
    public AudioSource meleeAudioSrc;

    protected override void Start()
    {
        //Inherits Start method from Weapon.cs
        base.Start();

        //You're only supposed to attack if you click, so the bool for attacking is set to false from the beginning.
        isAttacking = false;
    }

    protected override void Update()
    {
        //Inherits Update method from Weapon.cs
        base.Update();

        //If the left mouse button has been released, you should no longer be in attack mode
        if (Input.GetMouseButtonUp(0))
            isAttacking = false;

        //Flips the sprite to simulate an attack (for the time being).
        //In the future this should be replaced by an animation,
        //or a more appropriate visual representation of the player character using the sword or knife to attack an enemy.
        if (isAttacking == false)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (isAttacking == true)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public override void Attack()
    {
        //Inherits Attack method from Weapon.cs
        base.Attack();
        meleeAudioSrc.Play();

        //Sets the attack bool to true
        isAttacking = true;
    }
}
