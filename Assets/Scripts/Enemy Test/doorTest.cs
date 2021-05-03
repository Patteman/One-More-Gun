using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            gameObject.SetActive(false);
        }
    }
}
