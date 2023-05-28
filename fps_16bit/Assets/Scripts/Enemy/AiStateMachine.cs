using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
    public class AiStateMachine
    {
        public AiState[] aiStates;
        public AiAgent agent;
        public AiStateId currentState;

        public AiStateMachine(AiAgent agent)
        {
            this.agent = agent;
            int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
            aiStates = new AiState[numStates];
        }

        public void RegisterState(AiState state)
        {
            int index = (int)state.GetId();
            aiStates[index] = state;
        }

        public AiState GetState(AiStateId stateId)
        {
            int index = (int)stateId;
            return aiStates[index]; 
        }

        public void Update()
        {
            GetState(currentState)?.Update(agent);
        }

        public void ChangeState(AiStateId newState)
        {
            GetState(currentState)?.Exit(agent);
            currentState = newState;
            GetState(currentState)?.Enter(agent);
        }
    }
}

