using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTest : MonoBehaviour
{   
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            gameObject.SetActive(false);
        }
    }
}
