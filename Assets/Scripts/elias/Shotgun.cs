using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public int damageAmount;

    float distance;
    RaycastHit2D[] hits;

    public AudioSource shotgunAudioSrc;

    Camera mainCam;
    Vector3 direction;

    protected override void Start()
    {
        //Inherits start method from Weapon.cs
        base.Start();

        //Sets the amount of distance you are able to hit something from
        distance = 5f;

        //Sets the camera
        mainCam = Camera.main;
    }

    protected override void Update()
    {
        //Inherits Update method from Weapon.cs
        base.Update();

        //Converts the mouse position to a world position and thus allows you to aim.
        direction = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
    }
    public override void Attack()
    {
        //Inherits Attack method from Weapon.cs
        base.Attack();
        shotgunAudioSrc.Play();

        //angles for spreading out the shooting
        Quaternion angle1 = Quaternion.AngleAxis(-15, new Vector3(0, 1, 0));
        Quaternion angle2 = Quaternion.AngleAxis(15, new Vector3(0, 1, 0));

        //makes a RayCastHit from your position, the mouse position, and the maximum distance your "bullet" can travel.
        hits[0] = Physics2D.Raycast(transform.position, direction, distance);
        hits[1] = Physics2D.Raycast(transform.position, angle1 * direction, distance);
        hits[2] = Physics2D.Raycast(transform.position, angle2 * direction, distance);
        

        //Reduces health if hit target is an "enemy"
        foreach (RaycastHit2D hit in hits)
        {
           if (hit && hit.transform.gameObject.tag == "Enemies")
            {
                hit.transform.gameObject.SendMessage("TakeDamage", damageAmount);

                //used during test phase and should be removed eventually.
                Debug.Log("Hit a target");
            }
        }
    }
}
//Special thanks to CodeMonkey, FPS Builders, Anfractuous Dev for ideas and help in regards to Raycast
