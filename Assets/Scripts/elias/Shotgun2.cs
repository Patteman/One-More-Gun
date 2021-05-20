﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun2 : Weapon
{
    Vector3 direction;
    Camera mainCam;
    GameObject[] bullets;

    public GameObject bullet;
    public Transform bulletSpawnPoint;

    public AudioSource shotgunAudioSrc;

    protected override void Start()
    {
        base.Start();
        mainCam = Camera.main;
        bullets = new GameObject[3];
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
        shotgunAudioSrc.Play();

        direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.z = 0;
        direction.Normalize();

        ÍnstantiateBullets();

    }

    public override void EnemyAttack(Transform target)
    {
        base.EnemyAttack(target);
        shotgunAudioSrc.Play();

        direction = target.position - transform.position;
        direction.z = 0;
        direction.Normalize();

        ÍnstantiateBullets();
    }

    private void ÍnstantiateBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(bullet, bulletSpawnPoint);
        }

        //Converts the mouse position to a world position and thus allows you to aim.
        
        Quaternion angle1 = Quaternion.Euler(new Vector3(0, -15, 0));
        Quaternion angle2 = Quaternion.Euler(new Vector3(15, 15, 0));

        bullets[0].GetComponent<BulletScript>().Setup(direction);
        bullets[1].GetComponent<BulletScript>().Setup(angle1 * direction);
        bullets[2].GetComponent<BulletScript>().Setup(angle2 * direction);
    }
}