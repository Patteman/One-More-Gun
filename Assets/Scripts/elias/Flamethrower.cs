using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public ParticleSystem flameEffect;
    public Transform flameStartPos;

    Vector3 position;
    Camera mainCam;
    GameObject flame;
    //ParticleSystemRenderer partRend;
    void Start()
    {
        mainCam = Camera.main;
        //flameStartPos = new Vector3(transform.position.x + 0.46f, transform.position.y, transform.position.z);
        //flame2.Pause();
        //partRend = flameEffect.GetComponent<ParticleSystemRenderer>();
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
            //partRend.enabled = true;
            Fire();
        }
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    flameEffect.Stop(true);
        //}

    }
    void Fire()
    {
        //Vector3 direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //direction.Normalize();
        //flame2.Play();
        //flameEffect.SetActive(true);
        //bb
        /*Vector3 screenPoint = mainCam.WorldToScreenPoint(transform.localPosition);*/ //player position in screen space
        Vector3 mouseCameraPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 distance = Input.mousePosition-screenPoint; //distance between mouse and player
        //distance.Normalize();
        //float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg; //the angle, in degrees
        flame = Instantiate(flameEffect.gameObject, flameStartPos.position, Quaternion.identity);
        flame.transform.LookAt(mouseCameraPos);
    }
}
