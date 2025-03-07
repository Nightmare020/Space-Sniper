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
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
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
        StartAgent();
        target = newTarget;
    }
    public void StartAgent()
    {
        agent.isStopped = false;
    }
    public void StopAgent()
    {
        agent.isStopped= true;
        agent.velocity = Vector3.zero;
    }
}
