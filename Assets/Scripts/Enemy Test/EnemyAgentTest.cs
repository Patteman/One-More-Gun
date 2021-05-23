using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is out-of-date, and only old objects may use the script. See EnemyAI3 for most up-to-date script.

public class EnemyAgentTest : MonoBehaviour
{
    //States for enemy behavior
    private enum State
    {
        Roaming,
        Patrolling,
        StandingGuard,
        ChaseTarget,
        AttackTarget,
    }
    private State currentState;

    private enum Behavior
    {
        StandGuard,
        Patrol,
        Roam,
    }
    [Header("Pre-start Settings")]
    [SerializeField] private Behavior AIBehavior;
    
    public Transform target;
    public Transform firePoint;
    public EnemyGunScript enemyGunScript;

    [Header("Watch and Patrol Points")]
    [Tooltip("The point where enemy looks when stading guard")]
    [SerializeField] private Transform guardPoint;
    [Tooltip("The first point where enemy moves to when patrolling")]
    [SerializeField] private Transform patrolPointA;
    [Tooltip("The point where the enemy looks after moving to the first patrol point")]
    [SerializeField] private Transform patrolWatchPointA;
    [Tooltip("The second point where enemy moves to when patrolling")]
    [SerializeField] private Transform patrolPointB;
    [Tooltip("The point where the enemy looks after moving to the second patrol point")]
    [SerializeField] private Transform patrolWatchPointB;
    [Tooltip("How long the enemy waits before moving to next patrol point")]
    [Range(1,5)]
    [SerializeField] private float patrolWaitTime = 3f;

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 lookAt;
    private Vector3 guardPosition;

    private float currentSpeed;
    private float currentPatrolWaitTime;

    private bool moveToPointA;

    private float reachedPositionDistance = 1f;

    [Header("FOV Settings")]
    [SerializeField] private Transform fovPrefab;
    [SerializeField] private float fov;
    [SerializeField] private float viewDistance;
    private FieldOfView fieldOfView;

    [SerializeField] public LayerMask layerMask;

    [Header("Stats")]
    public float maxHealth = 100;
    public float health;
    public float fireRate = 1f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float runSpeed = 3.5f;
    private float fireCooldown = 0f;

    private NavMeshAgent agent;

    //private float turnSpeed;                //New

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();

        walkSpeed = 1f;
        runSpeed = 3.5f;
        currentSpeed = walkSpeed;
        currentPatrolWaitTime = patrolWaitTime;

        health = maxHealth;

        moveToPointA = true;

        guardPosition = transform.position;

        //Should not be present if you wonna use inspector to set
        fov = 90f;
        viewDistance = 5f;

        //Instansiates the field of view, with fov and view distance
        fieldOfView = Instantiate(fovPrefab, null).GetComponent<FieldOfView>();
        fieldOfView.SetFOV(fov);
        fieldOfView.SetViewDistance(viewDistance);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //Rotation
        //turnSpeed = 150f;     //NEW

        switch (AIBehavior)
        {
            default:
                currentState = State.StandingGuard;
                break;
            case Behavior.Roam:
                currentState = State.Roaming;
                break;
            case Behavior.Patrol:
                currentState = State.Patrolling;
                break;
            case Behavior.StandGuard:
                currentState = State.StandingGuard;
                break;
        }
    }

    private void Update()
    {
        if (PlayerController.isDead) //Stop update if player is dead (stops errors/exceptions)
        {
            return;
        }

        agent.speed = currentSpeed;

        //Turns so enemy looks as target
        var dir = lookAt - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                //NEW COMMETNED
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);    //NEW COMMENTED


        //Dette virker for modellen, men FOV'en er independent               //NEW
        //Vector3 myLocation = transform.position;
        //Vector3 targetLocation = roamPosition;
        //targetLocation.z = myLocation.z;

        //Vector3 vectorToTarget = targetLocation - myLocation;
        //Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

        //Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        //    From                  To                    Speed


        //State machine
        switch (currentState)
        {
            default:
            case State.StandingGuard:
                FindTarget();
                if(Vector3.Distance(transform.position, guardPosition) < reachedPositionDistance)
                {
                    lookAt = guardPoint.position;
                }
                else
                {
                    lookAt = guardPosition;
                    agent.SetDestination(guardPosition);
                }
                break;

            case State.Patrolling:
                if (moveToPointA)
                {
                    PatrolTo(patrolPointA.position, patrolWatchPointA.position);
                }
                else
                {
                    PatrolTo(patrolPointB.position, patrolWatchPointB.position);
                }
                break;
            
            case State.Roaming:
                //Move to target using the agent component(roamPos)
                agent.SetDestination(roamPosition);

                lookAt = roamPosition;    
                //lookAt = agent.nextPosition;   //NEW

                //Checks if player is in range
                FindTarget();

                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) //target hit, find next roam pos
                {
                    Debug.Log("Target hit");
                    roamPosition = GetRoamingPosition();
                }
                break;

            case State.ChaseTarget:
                //Move to target (player)
                agent.SetDestination(target.position);

                lookAt = target.transform.position;

                //Check if target is out of range
                DropTarget();

                float attackRange = 3f; //use weapon range here
                if (Vector3.Distance(transform.position, target.transform.position) < attackRange) //Player in weapon range, switch state
                {
                    currentState = State.AttackTarget;
                }
                break;

            case State.AttackTarget:
                lookAt = target.transform.position;

                //Shoots every second
                if (fireCooldown <= 0f)
                {
                    enemyGunScript.Shoot(target, firePoint);
                    fireCooldown = 1f / fireRate;
                }
                fireCooldown -= Time.deltaTime;

                //Checks if player is out of shooting range, then chases again
                float shootRange = 4.5f;
                if (Vector3.Distance(transform.position, target.transform.position) > shootRange) //Player outside weapon range, chase again
                {
                    currentState = State.ChaseTarget;
                }
                break;
        }

        fieldOfView.SetAimDirection(dir);  //Makes fov aim where the enemy is aiming
        fieldOfView.SetOrigin(transform.position);  //Makes fov follow enemy
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
        //-----------------------  USE ONLY THIS IF YOU DO NOT WANT VISIBLE FOV ----------------------//
        if (Vector3.Distance(transform.position, target.transform.position) < viewDistance)
        {
            //Player is inside view distance, check if inside view angle
            Vector3 dirToPlayer = (target.transform.position - transform.position).normalized;
            var dir = lookAt - transform.position;
            if (Vector3.Angle(dir, dirToPlayer) < fov / 2f)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance, layerMask);
                if(raycastHit2D.collider != null)
                {
                    Debug.Log(raycastHit2D.collider.tag);

                    if (raycastHit2D.collider.tag == "Player")
                    {
                        //Player hit
                        currentState = State.ChaseTarget;
                        currentSpeed = runSpeed;
                    }
                }
            }
        }
    }

    private void DropTarget() //Checks if it's target (player) is within range, switches state
    {
        float dropRange = 10f;
        if (Vector3.Distance(transform.position, target.transform.position) > dropRange)
        {
            currentSpeed = walkSpeed;

            switch (AIBehavior)
            {
                case Behavior.Roam:
                    GetRoamingPosition();
                    currentState = State.Roaming;
                    break;
                case Behavior.Patrol:
                    currentState = State.Patrolling;
                    break;
                case Behavior.StandGuard:
                    currentState = State.StandingGuard; //Currently just stops the enemy and makes it look in the direction of watchpoint.
                    //needs function for returning to guardpoint.
                    break;
            }

        }
    }

    public void TakeDamage(float dmg) //Method gets called when enemy is hit by attack
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        currentState = State.ChaseTarget;
        currentSpeed = runSpeed;
    }

    private void Die() //Method gets called when the enemy has to die
    {
        Destroy(gameObject);
        fieldOfView.DestroyFOV();
    }

    private void PatrolTo(Vector3 patrolPosition, Vector3 watchPosition)
    {
        lookAt = patrolPosition;
        agent.SetDestination(patrolPosition);
        FindTarget();
        if (Vector3.Distance(transform.position, patrolPosition) < reachedPositionDistance)
        {
            PatrolWatch(watchPosition);
        }
    }

    private void PatrolWatch(Vector3 patrolWatchPoint)
    {
        lookAt = patrolWatchPoint;
        FindTarget();
        if (currentPatrolWaitTime <= 0f)
        {
            moveToPointA = !moveToPointA;
            currentPatrolWaitTime = patrolWaitTime;
        }
        currentPatrolWaitTime -= Time.deltaTime;
    }

    // Elias: added this for potential flamethrower damage. Amount of damage is not final, but it works now, at least.
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Flame")
        {
            TakeDamage(0.5f);
        }
    }

}
