using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform spawnpoint;
    public GameObject impact;

    private int nrOfShots;
    public int maxShots;

    Camera mainCam;
    float distance;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        nrOfShots = 0;
        distance = 5f;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Converts the mouse position to a world position and thus allows you to aim.
        direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();

        //If you click, you will fire the weapon (as with every other weapon).
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        //if the amount of shots you've fired exceeds the maximum amount, the object will (should) be removed from your inventory.
        if (nrOfShots >= maxShots)
        {
            Destroy(gameObject);
        }
    }
    void Shoot()
    {
        //makes a RayCastHit from your position, the mouse position, and the maximum distance your "bullet" can travel.
        RaycastHit2D hit=Physics2D.Raycast(transform.position, direction, distance);
        if (hit)
        {
            Debug.Log("Hit Target");
        }
        nrOfShots++;
        Debug.Log(nrOfShots);
    }
}
//Special thanks to CodeMonkey, FPS Builders, Anfractuous Dev for ideas and help in regards to Raycast
