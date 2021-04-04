using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Rigidbody2D RB;
    private float speed;
    private Camera maincam;
    public GameObject[] weapons;
    private GameObject currentWeapon;
    public Transform weaponPos;
    private int index;

    void Start()
    {
        speed = 5;
        index = 0;
        Equip(index);
        maincam = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        //for(int i=0; i<weapons.Length; i++)
        //{
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //scrolls through the list of weapons. This list is mainly a test thing so far.
            index++;
            if (index == weapons.Length)
                index = 0;
            Equip(index);
        }
        //}
        RB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed; //we want to move horizontally and vertically

        Vector3 mouse = Input.mousePosition; //where the mouse is
        Vector3 screenPoint = maincam.WorldToScreenPoint(transform.localPosition); //player position in screen space

        Vector2 distance = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y); //distance between mouse and player
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg; //the angle, in degrees
        transform.rotation = Quaternion.Euler(0, 0, angle); //rotates the player
        //Debug.Log(currentWeapon);
    }

    void Equip(int i)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weapons[i], weaponPos.position, weaponPos.rotation);
        currentWeapon.transform.parent = weaponPos;
    }
}
//Special thanks to GamesPlusJames for help on movement and rotation.
//Special thanks to Sebastian Lague and David Täljsten for help on switching weapons.
