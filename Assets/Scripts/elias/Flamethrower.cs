using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    public ParticleSystem flameEffect;
    public AudioSource flamethrowerAudioSrc;

    //where we want the flame effect to start
    public Transform flameStartPos;

    Camera mainCam;
    Vector3 mouseCameraPos;
    GameObject flame;

    bool isActive;

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

        //Gets the World position of the mouse
        mouseCameraPos = mainCam.ScreenToWorldPoint(Input.mousePosition); //World position of mouse.

        while (isActive)
        {
            SetFlamePos();
        }
    }
    public override void Attack()
    {
        //Inherits Attack method from Weapon.cs
        base.Attack();
        flamethrowerAudioSrc.Play();
        
        //Instantiates a flame effect, and tells it to "look at" the mouse position.
        flame = Instantiate(flameEffect.gameObject, flameStartPos.position, Quaternion.identity);

        isActive = true;

    }
    public void SetFlamePos()
    {
        flame.transform.position = flameStartPos.position;
        flame.transform.LookAt(mouseCameraPos);
    }
}
//Special thanks to David Täljsten and Fat Pug Studio
