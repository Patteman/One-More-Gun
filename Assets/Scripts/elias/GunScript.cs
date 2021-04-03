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
        //Instantiates a bullet, obtains its script and sets the direction
        GameObject tempBullet = Instantiate(bullet, position, Quaternion.identity);
        BulletScript bs = tempBullet.GetComponent<BulletScript>();
        //direction is towards where you click
        Vector3 direction = maincam.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        direction.Normalize();

        if (bs != null)
        {
            //aims bullet at mouse position
            bs.Setup(direction);
            //counts amount of bullets fired, if it's above the maximum, the gun disappears
            bulletCounter++;
            if (bulletCounter >= ammunitionAmount)
            {
                Destroy(gameObject);
                bulletCounter = 0;
            }
        }
    }
}
