using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTest : MonoBehaviour
{
    public float speed = 1f;
    public float countdown = 0.5f;
    public float deathtimer = 10f;

    Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        deathtimer -= Time.deltaTime;
        countdown -= Time.deltaTime;
        pos.y += speed * Time.deltaTime;
        pos.x = -4.5f;
        pos.z = -1.5f;
        if (countdown <= 0f)
        {
            speed = speed * -1;
            countdown = 0.5f;
        }

        transform.position = pos;

        if (deathtimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
