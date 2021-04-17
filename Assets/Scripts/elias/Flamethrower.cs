using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    public ParticleSystem flameEffect;

    //where we want the flame effect to start
    public Transform flameStartPos;

    Camera mainCam;
    GameObject flame;

    protected override void Start()

    {
        //inherits Start method from Weapon.cs
        base.Start();

        //The main camera is needed to get the world position of the mouse.
        mainCam = Camera.main; 
    }
    
    protected override void Update()
    {
        //inherits Update method from Weapon.cs
        base.Update();

    }
    public override void Attack()
    {
        //Inherits Attack method from Weapon.cs
        base.Attack();

        //Gets the World position of the mouse
        Vector3 mouseCameraPos = mainCam.ScreenToWorldPoint(Input.mousePosition); //World position of mouse.
        
        //Instantiates a flame effect, and tells it to "look at" the mouse position.
        flame = Instantiate(flameEffect.gameObject, flameStartPos.position, Quaternion.identity);
        flame.transform.LookAt(mouseCameraPos);
    }
}
//Special thanks to David Täljsten
