using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using fps_16bit;

namespace fps_16bit
{
    public class EnemyLocomotion : MonoBehaviour
    {
        private static string Attack = "Attack";

        public Transform player;
        public Animator anim;

        public Player playerScript;

        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;
        private float attackDelay = 0.8f;

        public LayerMask whatIsGround; // to set ground to walk
        public LayerMask whatIsPlayer; // to set player layer 

        [SerializeField] private NavMeshAgent agent;

        [SerializeField] private EnemyLineOfSight sensor;

        public bool playerInSightRange;

        private bool isTakingDamage = false;

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
            playerInSightRange = sensor.playerVisible;

            anim.SetFloat("Speed", agent.velocity.magnitude);

            if (!playerInSightRange) Patroling(); // if player not in range, baseman will go random by search walk point
            if (playerInSightRange) ChasePlayer(); // if player in range, baseman will chase player

        }

        private void Patroling()
        {
            agent.speed = 3.5f;
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
            agent.speed = 5.0f;
            agent.SetDestination(player.position);

        }

        private void OnCollisionEnter(Collision collision)
        {
            transform.LookAt(player); // Make the enemy look at the player

            if (collision.gameObject.CompareTag("Player"))
            {
                if (!isTakingDamage)
                {

                    StartCoroutine(AttackOverTime(10, 1f)); // Decrease 10 health every 1 second
                }
                anim.SetTrigger(Attack);
            }
        }

        private IEnumerator AttackOverTime(int damageAmount, float duration)
        {
            isTakingDamage = true;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                playerScript.TakeDamage(damageAmount);

                elapsedTime += 1;

                yield return null;
            }

            isTakingDamage = false;
        }
    }

}
