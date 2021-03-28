using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public GameObject flameEffect;
    //public ParticleSystem flame2;
    Vector3 position;
    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        //flame2.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        
        //flame2.transform.position = position;
        //flame2.transform.rotation = Quaternion.Euler(0, 0, angle);
        //flameEffect.transform.position = transform.position;
        //flameEffect.transform.rotation = transform.rotation;
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        //else
        //    flame2.Pause();
    }
    void Fire()
    {
        Vector3 direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        //flame2.Play();
        //flameEffect.SetActive(true);
        //bb
        Vector3 mouse = Input.mousePosition; //where the mouse is
        Vector3 screenPoint = mainCam.WorldToScreenPoint(transform.localPosition); //player position in screen space

        Vector2 distance = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y); //distance between mouse and player
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg; //the angle, in degrees
        //aa
        GameObject flame = Instantiate(flameEffect, position, Quaternion.Euler(0, angle, 0));
    }
}
