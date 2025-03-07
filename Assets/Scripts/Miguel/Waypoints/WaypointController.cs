using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Waypoint characteristics")]
    [SerializeField] private Vector3 waypointTriggerArea;

    [Header("Hide")]
    [SerializeField] private bool isHidingSpot;
    [SerializeField] private int capacity;
    [SerializeField] private float deactivationDelay;
    [SerializeField] private Vector3 scareArea;


    WaypointController[] _ta = new WaypointController [1];

    [Header("Debug")]
    [SerializeField] private List<NPCAIController> hidingNpcs;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out NPCAIController npc))
        {
            if (isHidingSpot && !TryHide(npc))
            {
                _ta[0] = this;
                npc.ChangeTarget(WaypointManager.instance.GetRandomWaypoint(_ta).transform);
            }
            else
            {
                StartCoroutine(npc.ScheduleDeactivation(deactivationDelay));
            }
        }
    }

    public void ScareNpcs(Vector3 bullethitpos)
    {
       
    }

    private bool TryHide(NPCAIController npc)
    {
        if (hidingNpcs != null && hidingNpcs.Count < capacity)
        {
            hidingNpcs.Add(npc);
            return true;
        }
        return false;
    }


#if UNITY_EDITOR
    [SerializeField] private bool drawDebugArea;
    private void OnDrawGizmosSelected()
    {
        if (drawDebugArea)
        {
            Gizmos.color = new Color(0, 0, 1, 1f);
            Gizmos.DrawWireCube(transform.position, waypointTriggerArea);
            Gizmos.color = new Color(0, 0, 1, 0.2f);
            Gizmos.DrawCube(transform.position, waypointTriggerArea);

            Gizmos.color = new Color(1, 0, 0, 1f);
            Gizmos.DrawWireCube(transform.position, scareArea);
            Gizmos.color = new Color(1, 0, 0, 0.2f);
            Gizmos.DrawCube(transform.position, scareArea);
        }
    }
#endif
}
