using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Boat : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 targetPosition = Vector3.zero;
    NavMeshPath path;
    GameEvent gameEvent;
    
    [SerializeField] private float minSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out XRControllerSpeed controller))
        {
            HandAnimator handAnimator = other.GetComponentInChildren<HandAnimator>();
            if (!handAnimator) return;
            if (controller.GetControllerSpeed().magnitude > minSpeed && handAnimator.ClosedFist())
            {
                handAnimator.Rumble(1, 0.2f);
                Destroy(gameObject);
                gameEvent.RemoveEventFromQuarry();
            }
        }
    }

    private void Start()
    {
        gameEvent = new GameEvent();
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }
    /*
    private void FixedUpdate()
    {
        if (agent.CalculatePath(targetPosition, path) && targetPosition != Vector3.zero) return;
        /*foreach (var bridge in UnbreakableBridges.Bridges)
        {
            if (agent.CalculatePath(bridge.position, path))
            {
                agent.SetDestination(bridge.position);
                //Debug.Log(bridge.name);
                targetPosition = bridge.position;
                return;
            }
            //Debug.Log("no destination");
        }*/
        /*
        var bridge = UnbreakableBridges.Bridges[Random.Range(0, UnbreakableBridges.Bridges.Count)];
        if (agent.CalculatePath(bridge.position, path))
        {
            agent.SetDestination(bridge.position);
            //Debug.Log(bridge.name);
            targetPosition = bridge.position;
        }
    }*/
}
