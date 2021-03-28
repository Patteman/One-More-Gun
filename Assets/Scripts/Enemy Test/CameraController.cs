using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float zoomSize = 1;

    void Update()
    {
        //Follow player
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        //Zoom in and out
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoomSize>2)
            zoomSize -= 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (zoomSize<10)
            zoomSize += 1;
        }
        GetComponent<Camera>().orthographicSize = zoomSize;

    }


}
