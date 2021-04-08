using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public bool setOff;
    float timer;
    public GameObject expEffect;

    void Start()
    {
        //Bomb should not be activated, nor count down, unless it has been set off.
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (setOff == true)
        {
            timer += Time.deltaTime; //adds time in seconds.
            if (timer >= 5) //if five or more seconds have passed...
            {
                //the bomb will explode (be destroyed) and an explosion effect will play.
                GameObject explosion = Instantiate(expEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
