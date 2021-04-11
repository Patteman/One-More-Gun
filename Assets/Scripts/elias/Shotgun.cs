using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    
    //private int nrOfShots;
    //public int maxShots;
    public int damageAmount;

    Camera mainCam;
    float distance;

    Vector3 direction;

    // Start is called before the first frame update
    protected override void Start()
    {
        //nrOfShots = 0;
        base.Start();
        distance = 5f;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        //Converts the mouse position to a world position and thus allows you to aim.
        direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();

        //If you click, you will fire the weapon (as with every other weapon).
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Shoot();
        //}
        //if the amount of shots you've fired exceeds the maximum amount, the object will (should) be removed from your inventory.
        //if (nrOfShots >= maxShots)
        //{
        //    Destroy(gameObject);
        //}
    }
    public override void Attack()
    {
        base.Attack();

        //makes a RayCastHit from your position, the mouse position, and the maximum distance your "bullet" can travel.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

        //This has been made with the test targets in my stage in mind.
        //In the future this should be replaced with the health of the enemies
        if (hit && hit.transform.gameObject.tag == "TARGET")
        {
            TargetHealthAndStuff tHealth = hit.transform.gameObject.GetComponent<TargetHealthAndStuff>();
            tHealth.health -= damageAmount;
            Debug.Log("Hit a target!");
        }

        //Debug.Log(nrOfAttacks);
    }
    //void Shoot()
    //{
    //    //makes a RayCastHit from your position, the mouse position, and the maximum distance your "bullet" can travel.
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

    //    //This has been made with the test targets in my stage in mind.
    //    //In the future this should be replaced with the health of the enemies
    //    if (hit && hit.transform.gameObject.tag == "TARGET")
    //    {
    //        TargetHealthAndStuff tHealth = hit.transform.gameObject.GetComponent<TargetHealthAndStuff>();
    //        tHealth.health -= damageAmount;
    //    }
    //    nrOfShots++;
    //    Debug.Log(nrOfShots);
    //}
}
//Special thanks to CodeMonkey, FPS Builders, Anfractuous Dev for ideas and help in regards to Raycast
