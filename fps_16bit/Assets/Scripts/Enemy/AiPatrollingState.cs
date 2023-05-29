using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
    public class AiPatrollingState : AiState
    {
        public Transform playerTransform;
        public AiStateId GetId()
        {
            return AiStateId.Patrolling;
        }

        public void Enter(AiAgent agent)
        {
            if(playerTransform == null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        public void Update(AiAgent agent)
        {
            if (!agent.navmeshAgent.hasPath)
            {
                WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
                Vector3 min = worldBounds.min.position;
                Vector3 max = worldBounds.max.position;

                Vector3 randPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y),
                    Random.Range(min.z, max.z));
                agent.navmeshAgent.destination = randPosition;
            }
        }

        public void Exit(AiAgent agent)
        {
            
        }
    }
}


