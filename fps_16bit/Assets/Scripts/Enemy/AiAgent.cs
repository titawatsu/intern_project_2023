using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using fps_16bit;

namespace fps_16bit
{
    public class AiAgent : MonoBehaviour
    {
        public AiStateMachine aiStateMachine;
        public AiStateId initialState;
        public NavMeshAgent navmeshAgent;
        public AiAgentConfig config;
        public EnemyLineOfSight sensor;
        private void Start()
        {
            navmeshAgent = GetComponent<NavMeshAgent>();
            sensor = GetComponent<EnemyLineOfSight>();
            
            aiStateMachine = new AiStateMachine(this);
            aiStateMachine.RegisterState(new AiChasePlayerState());
            aiStateMachine.RegisterState(new AiPatrollingState());
            aiStateMachine.ChangeState(initialState);
        }

        private void Update()
        {
            aiStateMachine.Update();
        }
    }
}
