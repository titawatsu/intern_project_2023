using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyLocomotion : MonoBehaviour
{
    public Transform player;
    public Animator anim;

    

    [SerializeField] private NavMeshAgent agent;
    
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
        agent.destination = player.position;
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
}
