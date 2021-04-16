using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TypeOfGun { regular, shotgun }
public class GunScript : Weapon
{
    Vector3 position;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    Quaternion rotation=Quaternion.identity;
    Camera maincam;
    
    //public TypeOfGun type; //not certain if is to be used
    //int bulletCounter;
    //public int ammunitionAmount;

    protected override void Start()
    {
        base.Start();
        maincam = Camera.main;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        position = transform.position; //the position should be the position of the gun
        //if (Input.GetMouseButtonDown(0)) //if you press space it should spawn and shoot a bullet
        //{
        //    Attack();
        //}

    }

    public override void Attack()
    {
        base.Attack();

        //Instantiates a bullet, obtains its script and sets the direction
        GameObject tempBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        BulletScript bs = tempBullet.GetComponent<BulletScript>();
        //direction is towards where you click
        Vector3 direction = maincam.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        direction.Normalize();

        if (bs != null)
        {
            //aims bullet at mouse position
            bs.Setup(direction);
        }
    }
}
