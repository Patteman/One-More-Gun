using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform spawnpoint;
    public GameObject impact;

    Camera mainCam;
    float distance;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        distance = 15f;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit2D hit=Physics2D.Raycast(transform.position, direction, distance);
        if (hit)
        {
            Debug.Log("Bruh mannen");
        }
        //Debug.DrawRay(transform.position, direction, Color.red);
        //RaycastHit2D hit2;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, Input.mousePosition);
    }
}
