using System;
using UnityEngine;
using UnityEngine.AI;

public class Boat : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 targetPosition = Vector3.zero;
    NavMeshPath path;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    private void FixedUpdate()
    {
        if (agent.CalculatePath(targetPosition, path) && targetPosition != Vector3.zero) return;
        foreach (var bridge in UnbreakableBridges.Bridges)
        {
            if (agent.CalculatePath(bridge.transform.position, path))
            {
                targetPosition = bridge.transform.position;
                agent.SetDestination(targetPosition);
                Debug.Log(bridge.name);
                return;
            }
            Debug.Log("no destination");
        }
    }
}
