using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I MADE A MISTAKE DURING THE CODE REVIEW
//THIS DOCUMENT IS NOT TO BE DELETED
//IT EXIST TO NOT HAVE THE EXPLOSION EFFECT PLAY FOREVER
// - Elias
public class Explosion : MonoBehaviour
{
    float timer;
    public int damageAmount;
    
    void Update()
    {
        //We want the effect to stop after having played once.
        timer += Time.deltaTime;
        if (timer >= 0.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            collision.gameObject.SendMessage("TakeDamage", damageAmount);
        }
    }
}
