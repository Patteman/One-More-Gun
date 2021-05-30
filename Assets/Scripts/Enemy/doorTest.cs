using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTest : MonoBehaviour
{   
    //Short script for opening a door in a level. This will be replaced by a key/button script later.
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            gameObject.SetActive(false);
        }
    }
}
