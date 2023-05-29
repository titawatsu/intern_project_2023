using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
    public enum AiStateId
    {
        ChasePlayer,
        Patrolling
    }

    public interface AiState
    {
        AiStateId GetId();
        void Enter(AiAgent agent);
        void Update(AiAgent agent);
        void Exit(AiAgent agent);
    }
}