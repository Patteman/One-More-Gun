using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : Weapon
{
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    Camera maincam;

    public AudioSource gunSoundSrc;

    protected override void Start()
    {
        //inherits Start method from Weapon.cs
        base.Start();

        //Sets the camera
        maincam = Camera.main;
    }

    protected override void Update()
    {
        //Inherits Update method from Weapon.cs
        base.Update();
    }

    public override void Attack()
    {
        //Inherits Attack method from Weapon.cs
        
        base.Attack();
        gunSoundSrc.Play();
        //Instantiates a bullet and obtains its script.
        GameObject tempBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        BulletScript bs = tempBullet.GetComponent<BulletScript>();

        //The direction of the bullet is towards where you click.
        Vector3 direction = maincam.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        direction.Normalize();

        //If the script exists...
        if (bs != null)
        {
            //...bullet aims at mouse position upon click
            bs.Setup(direction);
        }
    }
}
