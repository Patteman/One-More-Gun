﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public int damageAmount;

    Camera mainCam;
    float distance;
    
    public AudioSource shotgunAudioSrc;

    Vector3 direction;
    
    protected override void Start()
    {
        //Inherits start method from Weapon.cs
        base.Start();

        //Sets the amount of distance you are able to hit something from
        distance = 5f;

        //Sets the camera
        mainCam = Camera.main;
    }

    protected override void Update()
    {
        //Inherits Update method from Weapon.cs
        base.Update();
        
        //Converts the mouse position to a world position and thus allows you to aim.
        direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
    }
    public override void Attack()
    {
        //Inherits Attack method from Weapon.cs
        base.Attack();
        shotgunAudioSrc.Play();

        //makes a RayCastHit from your position, the mouse position, and the maximum distance your "bullet" can travel.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

        //Reduces health if hit target is an "enemy"
        if (hit && hit.transform.gameObject.tag == "Enemies")
        {
            hit.transform.gameObject.SendMessage("TakeDamage", damageAmount);

            //used during test phase and should be removed eventually.
            Debug.Log("Hit a target");
        }
    }
}
//Special thanks to CodeMonkey, FPS Builders, Anfractuous Dev for ideas and help in regards to Raycast
