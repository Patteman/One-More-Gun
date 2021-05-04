using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTest : MonoBehaviour
{
    // Update is called once per frame
    
    private enum Behavior
    {
        Roam,
        Patrol,
        StandGuard,
    }
    [SerializeField]
    private Behavior AIBehavior;
    
    private bool roam;
    private bool patrol;
    private bool standGuard;

    void Start()
    {
        switch (AIBehavior)
        {
            default:
                roam = false;
                patrol = false;
                standGuard = true;
                break;
            case Behavior.Roam:
                roam = true;
                patrol = false;
                standGuard = false;
                break;
            case Behavior.Patrol:
                roam = false;
                patrol = true;
                standGuard = false;
                break;
            case Behavior.StandGuard:
                roam = false;
                patrol = false;
                standGuard = true;
                break;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            gameObject.SetActive(false);
        }
    }
}
