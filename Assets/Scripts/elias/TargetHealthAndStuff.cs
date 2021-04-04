using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealthAndStuff : MonoBehaviour
{
    public int health;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}
