using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private Transform target;

    private NavMeshAgent agent;
    private Transform _target;
    public void Init()
    {
        agent= GetComponent<NavMeshAgent>();
        target = WaypointManager.instance.GetRandomWaypoint().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != target)
        {
            _target= target;
            agent.destination = target.position;
        }
    }

    public void ChangeSpeed(float speed)
    {
        agent.speed = speed;
    }

    public void ChageTarget(Transform newTarget)
    {
        target = newTarget;
        StartAgent();
    }
    public void StartAgent()
    {
        agent.isStopped = false;
        agent.ResetPath();
    }
    public void StopAgent()
    {
        agent.isStopped= true;
        agent.velocity = Vector3.zero;
    }

    public bool IsTarget(Transform controller)
    {
        return target == controller;
    }
}
