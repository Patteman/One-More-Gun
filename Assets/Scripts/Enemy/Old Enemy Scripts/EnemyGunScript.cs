using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    //As of the implementation of the EnemyAI3 script, this script is not used anymore. Only old models and scripts still use this.

    public GameObject bullet;
    int bulletCounter;
    public int ammunitionAmount;
    Quaternion rotation=Quaternion.identity;

    public void Shoot(Transform target, Transform pointToShoot)
    {
        //Instaciates the bullet, gets the scripts and calculates the direction for the bullet.
        GameObject tempBullet = (GameObject)Instantiate(bullet, pointToShoot.position, Quaternion.identity);
        BulletScript bs = tempBullet.GetComponent<BulletScript>();
        Vector3 direction = target.position - pointToShoot.position;
        direction.Normalize();

        //Sends the direction vector to the bullet script
        if (bs != null)
        {
            bs.Setup(direction);
        }
    }
}
