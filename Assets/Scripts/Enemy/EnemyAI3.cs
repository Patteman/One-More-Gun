using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI3 : MonoBehaviour
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

    //States for enemy orders
    private enum Behavior
    {
        StandGuard,
        Patrol,
        Roam,
    }

    [Header("Pre-start Settings")]
    [SerializeField] private Behavior AIBehavior;
    
    public Transform target;
    public GameObject hand;
    private WeaponInventory inventory;
    public AudioSource enemyAudioSrc;

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
    private float reachedPositionDistance = 1f;
    private float destructionTimer;

    private bool moveToPointA;
    private bool madeSound;

    [Header("FOV Settings")]
    [SerializeField] private Transform fovPrefab;
    [SerializeField] private float fov;
    [SerializeField] private float viewDistance;
    private FieldOfView fieldOfView;

    [SerializeField] public LayerMask layerMask;

    [Header("Stats")]
    public float maxHealth = 100;
    public float health;
    public float fireRate = 3f;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float fireCooldown = 0f;

    private NavMeshAgent agent;

    private void Start()
    {
        startingPosition = transform.position;
        guardPosition = transform.position;
        roamPosition = GetRoamingPosition();

        walkSpeed = 1f;
        runSpeed = 3.5f;
        currentSpeed = walkSpeed;
        currentPatrolWaitTime = patrolWaitTime;
        
        health = maxHealth;

        fov = 90f;
        viewDistance = 5f;

        moveToPointA = true;

        //Instansiates the field of view, with fov and view distance
        fieldOfView = Instantiate(fovPrefab, null).GetComponent<FieldOfView>();
        fieldOfView.SetFOV(fov);
        fieldOfView.SetViewDistance(viewDistance);

        //Default code needed for agent script to work
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //Switch to set behavior at start depending on order given
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
        inventory = gameObject.GetComponent<WeaponInventory>();

        StartCoroutine(LateStart(0.05f));
    }

    //Coroutine to make sure the inventory script's start method is complete before method is called.
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        chooseRandomWeapon();
    }


    private void Update()
    {
        //Stop update if player is dead (stops errors/exceptions)
        if (PlayerController.isDead)
        {
            return;
        }

        //Keeps the agent script's own speed viariable updated with the speed given in this script
        agent.speed = currentSpeed;

        //Rotates enemy to look at the "lookAt" position
        var dir = lookAt - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //State machine
        switch (currentState)
        {
            default:
            case State.StandingGuard:
                FindTarget();
                
                //If enemy is at the guard postition then look at guard point else move to that position first
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
                //Move to and look at the roam position
                agent.SetDestination(roamPosition);
                lookAt = roamPosition;    

                FindTarget();

                //Roam position reached, find new position
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    roamPosition = GetRoamingPosition();
                }
                break;

            case State.ChaseTarget:
                //Move to and look at the target (player)
                agent.SetDestination(target.position);
                lookAt = target.transform.position;

                DropTarget();

                //If target (player) is within attack range, start attacking
                float attackRange = 3f;
                if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
                {
                    currentState = State.AttackTarget;
                }
                break;

            case State.AttackTarget:
                lookAt = target.transform.position;

                Attack();

                //Checks if player is out of shooting range, then chases again
                float shootRange = 4.5f;
                if (Vector3.Distance(transform.position, target.transform.position) > shootRange)
                {
                    currentState = State.ChaseTarget;
                }
                break;
        }


        if (health <= 0)
        {
            health = 0;

            destructionTimer += Time.fixedDeltaTime;

            if (!madeSound)
            {
                enemyAudioSrc.Play();
                madeSound = true;
            }

            if (destructionTimer >= 0.9)
            {
               // Die();
            }
        }

        //Makes FOV cone follow the enemy and aim in the direction the enemy is looking
        fieldOfView.SetAimDirection(dir);
        fieldOfView.SetOrigin(transform.position);
    }

    //Calls equipped weapon's attack method
    private void Attack()
    {
        Weapon weaponScript = hand.GetComponentInChildren<Weapon>();
        if(fireCooldown <= 0f)
        {
            try
            {
                weaponScript.EnemyAttack(target);
                fireCooldown = 0.75f;
            }
            catch
            {
                Debug.Log("Weapon script not found");
            }
        }
        fireCooldown -= Time.deltaTime;
    }

    //Gets a random position from the enemys current position, for it to roam to
    private Vector3 GetRoamingPosition() 
    {
        return startingPosition + GetRandomDir() * Random.Range(5f, 5f);
    }

    //Gets a random x and a random y and normalized the vector
    private Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    //Checks if it's target (player) is within range, switches state
    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < viewDistance)
        {
            //Player is inside view distance, check if inside view angle
            Vector3 dirToPlayer = (target.transform.position - transform.position).normalized;
            var dir = lookAt - transform.position;
            if (Vector3.Angle(dir, dirToPlayer) < fov / 2f)
            {
                //Player is inside angle, check if enemy can see the player (not behind wall)
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance, layerMask);
                if(raycastHit2D.collider != null)
                {
                    Debug.Log(raycastHit2D.collider.tag);

                    //If the object seen in the player, then switch state, if not, then it is a wall
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

    //Checks if it's target (player) is out-of-range, switches state
    private void DropTarget()
    {
        float dropRange = 10f;
        if (Vector3.Distance(transform.position, target.transform.position) > dropRange)
        {
            currentSpeed = walkSpeed;

            //Return to original behavior depending on the start order
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
                    currentState = State.StandingGuard;
                    break;
            }

        }
    }

    //Method gets called when enemy is hit by an attack
    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }

        //If hit, enemy will start chasing the target (player)
        currentState = State.ChaseTarget;
        currentSpeed = runSpeed;
    }

    //Destroys enemy object as well as its FOV cone.
    private void Die()
    {
        inventory.DropWeapon();
        Destroy(gameObject);
        fieldOfView.DestroyFOV();
    }

    //Method to make a patrolling enemy move to the next position
    private void PatrolTo(Vector3 patrolPosition, Vector3 watchPosition)
    {
        lookAt = patrolPosition;
        agent.SetDestination(patrolPosition);
        FindTarget();

        //If enemy reaches that position, start standing guard
        if (Vector3.Distance(transform.position, patrolPosition) < reachedPositionDistance)
        {
            PatrolWatch(watchPosition);
        }
    }

    //Enemy looks at a specific point for a certain amount of time, before moving to next patrol point.
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

    //Chooses a random weapon in the enemy weapon list
    private void chooseRandomWeapon()
    {
        int weaponNumber = Random.Range(0, inventory.inventoryList.Count);
        inventory.selectedWeapon = weaponNumber;
        inventory.SelectWeapon();
    }

    // This allows the enemy to get hurt by the flamethrower effect.
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Flame")
        {
            TakeDamage(0.5f);
        }
    }


}
