using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = 2.5f;
    }

    void Update()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
    }
}
