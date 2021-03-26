using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    public float speed = 10f;
    public float countdown = 0.5f;

    Vector3 pos;

    //Makes cube move left and right to test pause function
    void Update()
    {
        countdown -= Time.deltaTime;
        pos.x += speed * Time.deltaTime;
        if (countdown <= 0f)
        {
            speed = speed * -1;
            countdown = 0.5f;
        }

        transform.position = pos;
    }
}
