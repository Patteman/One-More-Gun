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
    private Vector3 lookAt;

    private float currentSpeed;
    private float roamSpeed;
    private float chaseSpeed;
    public Transform target;

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        roamSpeed = 1f;
        chaseSpeed = 2f;
        currentSpeed = roamSpeed;
        currentState = State.Roaming;
    }

    private void Update()
    {
        var dir = lookAt - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        switch (currentState)
        {
            default:
            case State.Roaming:
                //Move to target
                Vector3 direction = roamPosition - transform.position;
                transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);

                lookAt = roamPosition;
                FindTarget();

                float reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) //target hit, find next roam pos
                {
                    roamPosition = GetRoamingPosition();
                }
            break;

            case State.ChaseTarget:
                //Move to target (player)
                Vector3 targetdirection = target.transform.position - transform.position;
                transform.Translate(targetdirection.normalized * currentSpeed * Time.deltaTime, Space.World);

                lookAt = target.transform.position;
                DropTarget();

                float attackRange = 2f; //use weapon range here
                if (Vector3.Distance(transform.position, target.transform.position) < attackRange) //Player in weapon range, switch state
                {
                    currentState = State.AttackTarget;
                }
                break;

            case State.AttackTarget:
                lookAt = target.transform.position;
                //Attack with weapon
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
            currentSpeed = chaseSpeed;
        }
    }

    private void DropTarget() //Checks if it's target (player) is within range, switches state
    {
        float dropRange = 10f;
        if (Vector3.Distance(transform.position, target.transform.position) > dropRange)
        {
            GetRoamingPosition();
            currentSpeed = roamSpeed;
            currentState = State.Roaming;
        }
    }
}
