using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorAiTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorAi Door;
    private int AgentsInRange = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            
            AgentsInRange++;
            if (!Door.IsOpen)
            {
                Door.Open(other.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            // if you do not want to automatically close doors, do not implement this method
            AgentsInRange--;
            if (Door.IsOpen && AgentsInRange == 0)
            {
                Door.Close();
            }
        }
    }
}
