using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is out-of-date, and only old objects may use the script. See EnemyAI3 for most up-to-date script.

public class EnemyAI : MonoBehaviour
{
    //States for enemy behavior
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
    public Transform firePoint;
    public EnemyGunScript enemyGunScript;

    [SerializeField] private Transform fovPrefab;
    [SerializeField] private float fov;
    [SerializeField] private float viewDistance;
    private FieldOfView fieldOfView;

    [Header("Stats")]
    public float maxHealth = 100;
    public float health;
    public float fireRate = 1f;
    private float fireCooldown = 0f;

    [SerializeField] public LayerMask layerMask;

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();

        roamSpeed = 1f;
        chaseSpeed = 2f;
        currentSpeed = roamSpeed;

        currentState = State.Roaming;

        health = maxHealth;

        fov = 90f;
        viewDistance = 5f;

        //Instansiates the field of view, with fov and view distance
        fieldOfView = Instantiate(fovPrefab, null).GetComponent<FieldOfView>();
        fieldOfView.SetFOV(fov);
        fieldOfView.SetViewDistance(viewDistance);
    }

    private void Update()
    {
        if (PlayerController.isDead) //Stop update if player is dead (stops errors/exceptions)
        {
            return;
        }
        
        //Turns so enemy looks as target
        var dir = lookAt - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //State machine
        switch (currentState)
        {
            default:
            case State.Roaming:
                //Move to target (roamPos)
                Vector3 direction = roamPosition - transform.position;
                transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);

                lookAt = roamPosition;
                //Checks if player is in range
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
                        currentSpeed = chaseSpeed;
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
            GetRoamingPosition();
            currentSpeed = roamSpeed;
            currentState = State.Roaming;
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
    }

    private void Die() //Method gets called when the enemy has to die
    {
        Destroy(gameObject);
        //Other stuff than just destroy
    }

}
