using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionScript : MonoBehaviour
{
    private float coverHealth;
    public float maxHealth;
    private float destructionTimer;
    public AudioSource protectionAudioSrc;

    private bool madeSound;

    void Start()
    {
        coverHealth = maxHealth;
        madeSound = false;
    }
    
    void Update()
    {
        if (coverHealth <= 0)
        {
            destructionTimer += Time.fixedDeltaTime;

            if (!madeSound)
            {
                protectionAudioSrc.Play();
                madeSound = true;
            }

            if (destructionTimer >= 1.1f)
            {
                Destroy(gameObject);
            }
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
