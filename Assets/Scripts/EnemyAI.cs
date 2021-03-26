using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Create states for the enemy
    private enum State
    {
        Roaming,
        ChaseTarget,
        AttackTarget,
    }
    private State currentState;

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float speed;
    public Transform target;

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        speed = 2;
        currentState = State.Roaming;
    }

    private void Update()
    {
        FindTarget();
        switch (currentState)
        {
            default:
            case State.Roaming:
                //Move to target
                Vector3 direction = roamPosition - transform.position;
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

                float reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) //target hit, find next roam pos
                {
                    roamPosition = GetRoamingPosition();
                }
            break;

            case State.ChaseTarget:
                //Move to target (player)
                Vector3 targetdirection = target.transform.position - transform.position;
                transform.Translate(targetdirection.normalized * speed * Time.deltaTime, Space.World);

                float attackRange = 2f; //use weapon range here
                if (Vector3.Distance(transform.position, target.transform.position) < attackRange) //Player in weapon range, switch state
                {
                    currentState = State.AttackTarget;
                }
                break;

            case State.AttackTarget:
                //attack the target
            break;
        }
    }
    
    private Vector3 GetRoamingPosition() //Gets random position to roam to
    {
        return startingPosition + GetRandomDir() * Random.Range(5f, 5f);
    }

    private Vector3 GetRandomDir() //Gets a random x and a random y and normalized the vector
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void FindTarget() //Checks if it's target (player) is within range, switches state
    {
        float targetRange = 6f;
        if (Vector3.Distance(transform.position, target.transform.position) < targetRange)
        {
            currentState = State.ChaseTarget;
        }
    }
}
