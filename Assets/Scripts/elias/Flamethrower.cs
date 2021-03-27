using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public GameObject flameEffect;

    Vector3 position;
    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    void Fire()
    {
        Vector3 direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        GameObject flame = Instantiate(flameEffect, transform.position, transform.rotation);
    }
}
