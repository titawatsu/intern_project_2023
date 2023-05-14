using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class DoorAi : MonoBehaviour
{
    //[SerializeField] BoxCollider doorCollider;
    private Coroutine AnimationCoroutine;
    private NavMeshObstacle Obstacle;
    private DoorManager doorManager;
    public bool IsOpen = false;
    public float Speed = 1f;

    private void Awake()
    {
        doorManager = GetComponentInChildren<DoorManager>();
        Obstacle = GetComponent<NavMeshObstacle>();

        Obstacle.carveOnlyStationary = false;
        Obstacle.carving = IsOpen;
        Obstacle.enabled = IsOpen;
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(DoRotationOpen());
        }
    }

    private IEnumerator DoRotationOpen()
    {
        IsOpen = true;

        doorManager.State = DoorManager.DoorState.Opened;

        float time = 0;
        while (time < 1)
        {
            yield return null;
            time += Time.deltaTime * Speed;
        }

        Obstacle.enabled = true;
        Obstacle.carving = true;
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        Obstacle.carving = false;
        Obstacle.enabled = false;

        IsOpen = false;
        doorManager.State = DoorManager.DoorState.Closed;

        float time = 0;
        while (time < 1)
        {
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}