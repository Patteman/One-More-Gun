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
        direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (nrOfShots >= maxShots)
        {
            Destroy(gameObject);
        }
    }
    void Shoot()
    {
        RaycastHit2D hit=Physics2D.Raycast(transform.position, direction, distance);
        if (hit)
        {
            Debug.Log("Hit Target");
        }
        nrOfShots++;
        Debug.Log(nrOfShots);
        //Debug.DrawRay(transform.position, direction, Color.red);
        //RaycastHit2D hit2;
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawRay(transform.position, Input.mousePosition);
    //}
}
