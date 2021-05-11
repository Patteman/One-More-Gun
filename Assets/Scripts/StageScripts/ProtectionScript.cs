using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionScript : MonoBehaviour
{
    public float coverHealth;
    public float maxHealth;
    public AudioSource protectionAudioSrc;

    void Start()
    {
        coverHealth = maxHealth;
    }
    
    void Update()
    {
        
        if (coverHealth <= 0)
        {
            protectionAudioSrc.Play();
            Destroy(gameObject);
        }
    }

    #region Collision logic.

    /// <summary>
    /// Simple collision logic. If whatever gameobject with a specific tag collides with the "Protection" it takes different amounts of damage.
    /// </summary>
    /// <param name="other"></param>

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BasicProjectiles")
        {
            coverHealth -= 20f;
        }

        if (other.gameObject.tag == "RocketProjectile")
        {
            coverHealth -= 50f;
        }

        if (other.gameObject.tag == "POISONOUS")
        {
            coverHealth -= 10f;
        }
    }

    #endregion

}
