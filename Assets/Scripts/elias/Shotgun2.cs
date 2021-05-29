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
    public GameObject gunfireAnim;
    float timer;

    public AudioSource shotgunAudioSrc;

    protected override void Start()
    {
        base.Start();
        mainCam = Camera.main;

        // This should contain the projectiles fired
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

        // Converts the mouse position to a world position and thus allows you to aim.
        direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.z = 0;
        direction.Normalize();

        // Instantiates the animation and bullets.
        Instantiate(gunfireAnim, bulletSpawnPoint.position, Quaternion.identity);
        ÍnstantiateBullets();

    }

    public override void EnemyAttack(Transform target)
    {
        base.EnemyAttack(target);
        shotgunAudioSrc.Play();

        // Enemy should aim at player, not the mouse.
        direction = target.position - transform.position;
        direction.z = 0;
        direction.Normalize();

        // Calls on a method to instantiate a bullet.
        ÍnstantiateBullets();
    }

    private void ÍnstantiateBullets()
    {
        // Will create a bullet for each place in the array.
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(bullet, bulletSpawnPoint);
        }

        // The bullets should ideally go in different directions. 
        Quaternion angle1 = Quaternion.Euler(new Vector3(0, -15, 0));
        Quaternion angle2 = Quaternion.Euler(new Vector3(15, 15, 0));

        //Sets up the bullets.
        bullets[0].GetComponent<BulletScript>().Setup(direction);
        bullets[1].GetComponent<BulletScript>().Setup(angle1 * direction);
        bullets[2].GetComponent<BulletScript>().Setup(angle2 * direction);
    }
}
