using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using fps_16bit;

namespace fps_16bit
{
    public class AiChasePlayerState : AiState
    {

        public Transform playerTransform;
        float timer = 0.0f;
        public AiStateId GetId()
        {
            return AiStateId.ChasePlayer;
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
            if (!agent.enabled)
            {
                return;
            }
            timer -= Time.deltaTime;
            if (!agent.navmeshAgent.hasPath)
            {
                agent.navmeshAgent.destination = playerTransform.position;
            }
            if (timer < 0.0f)
            {
                Vector3 direction = playerTransform.position - agent.navmeshAgent.destination;
                direction.y = 0;
                if(direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
                {
                    if(agent.navmeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                    {
                        agent.navmeshAgent.destination = playerTransform.position;
                    }
                }
                timer = agent.config.maxTime;
            }
       }
        public void Exit(AiAgent agent)
        {
            throw new System.NotImplementedException();
        }
    }
}
