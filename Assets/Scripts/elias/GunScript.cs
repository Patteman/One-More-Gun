using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TypeOfGun { regular, shotgun }
public class GunScript : MonoBehaviour
{
    Vector3 position;
    //public TypeOfGun type; //not certain if is to be used
    public GameObject bullet;
    int bulletCounter;
    public int ammunitionAmount;
    Quaternion rotation=Quaternion.identity;
    Camera maincam;

    void Start()
    {
        maincam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position; //the position should be the position of the gun
        if (Input.GetMouseButtonDown(0)) //if you press space it should spawn and shoot a bullet
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject tempBullet = (GameObject)Instantiate(bullet, position, Quaternion.identity); //spawns a bullet
        BulletScript bs = tempBullet.GetComponent<BulletScript>(); //obtains the script from the spawned bullet
        Vector3 direction = maincam.ScreenToWorldPoint(Input.mousePosition)-transform.position; //SHOULD BE the direction the gun is pointed in
        direction.Normalize();

        if (bs != null)
        {
            bs.Setup(direction);
            bulletCounter++;
            if (bulletCounter >= ammunitionAmount)
            {
                Destroy(gameObject);
                bulletCounter = 0;
            }
        }
    }
}
