using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //Flips the sprite to simulate an attack.
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

    public override void EnemyAttack(Transform target)
    {
        base.EnemyAttack(target);
        meleeAudioSrc.Play();
        isAttacking = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemies take damage upon being hit.
        if (isAttacking == true && collision.gameObject.tag == "Enemies")
        {
            collision.gameObject.SendMessage("TakeDamage", damageAmount);
        }
    }
}
