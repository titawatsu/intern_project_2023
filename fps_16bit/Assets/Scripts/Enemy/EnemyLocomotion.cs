using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyLocomotion : MonoBehaviour
{
    public Transform player;
    public Animator anim;
    public EnemyLineOfSight lineOfSight;

    public GameObject gameOverUi;

    public Player playerScript;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public LayerMask whatIsGround; // to set ground to walk
    public LayerMask whatIsPlayer; // to set player layer 

    [SerializeField] private NavMeshAgent agent;

    public bool playerInSightRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //anim.SetFloat("Speed", agent.velocity.magnitude);

        //if (!playerInSightRange) Patroling(); // if player not in range, baseman will go random by search walk point
        //if (playerInSightRange) ChasePlayer(); // if player in range, baseman will chase player
    }

    private void CheckState()
    {
        if (lineOfSight.IsInSight(player.gameObject))
        {
            playerInSightRange = true;
        }
        else
        {
            playerInSightRange = false;
        }
    }
    

    private void Patroling()
    {

        if (!walkPointSet) SearchWalkPoint(); // to find walk point

        if (walkPointSet) agent.SetDestination(walkPoint); // when having walk point, baseman will go to that point.

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 5f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;

    }

    private void ChasePlayer() // go to player's position when player in sight
    {
        agent.SetDestination(player.position);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // check that baseman collided player already or not.
        {
            UnlockCursor();

            Time.timeScale = 0f;
        }
    }

    private void UnlockCursor() // for unlock cursor 
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
