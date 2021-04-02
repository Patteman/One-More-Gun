using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    public GameObject bullet;
    int bulletCounter;
    public int ammunitionAmount;
    Quaternion rotation=Quaternion.identity;

    public void Shoot(Transform target, Transform pointToShoot)
    {
        GameObject tempBullet = (GameObject)Instantiate(bullet, pointToShoot.position, Quaternion.identity); //spawns a bullet
        BulletScript bs = tempBullet.GetComponent<BulletScript>(); //obtains the script from the spawned bullet
        Vector3 direction = target.position - pointToShoot.position; //SHOULD BE the direction the gun is pointed in
        direction.Normalize();

        if (bs != null)
        {
            bs.Setup(direction);
            //bulletCounter++;
            //if (bulletCounter >= ammunitionAmount)
            //{
            //    Destroy(gameObject);
            //    bulletCounter = 0;
            //}
        }
    }
}
