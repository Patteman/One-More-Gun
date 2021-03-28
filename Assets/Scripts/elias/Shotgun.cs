using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform spawnpoint;
    public GameObject impact;

    Camera mainCam;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        distance = 15f;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Shoot()
    {
        RaycastHit2D hit;
        RaycastHit2D hit2;

        //if(Physics2D.Raycast())
    }
}
