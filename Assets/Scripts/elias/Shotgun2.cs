using System.Collections;
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

        bullets[0] = Instantiate(bullet, bulletSpawnPoint);
        bullets[1] = Instantiate(bullet, bulletSpawnPoint);
        bullets[2] = Instantiate(bullet, bulletSpawnPoint);

        //Converts the mouse position to a world position and thus allows you to aim.
        direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.z = 0;
        direction.Normalize();

        Quaternion angle1 = Quaternion.AngleAxis(-15, new Vector3(0, 1, 0));
        Quaternion angle2 = Quaternion.AngleAxis(15, new Vector3(0, 1, 0));

        bullets[0].GetComponent<BulletScript>().Setup(direction);
        bullets[1].GetComponent<BulletScript>().Setup(angle1 * direction);
        bullets[2].GetComponent<BulletScript>().Setup(angle2 * direction);

    }
}
