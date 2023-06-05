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
            Debug.Log("Found Agent");
            AgentsInRange++;
            Debug.Log("Add Agent");
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
            // agent automatically close the door
            AgentsInRange--;
            Debug.Log("Delete Agent");
            if (Door.IsOpen && AgentsInRange == 0)
            {
                Door.Close();
            }
        }
    }
}
