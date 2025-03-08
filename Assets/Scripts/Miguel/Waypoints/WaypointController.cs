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
    [SerializeField] private float scareRadius;
    [SerializeField] private float hideCooldown;

    [SerializeField]private bool cooldDown = false;

    WaypointController[] _ta = new WaypointController [1];

    [Header("Debug")]
    [SerializeField] private List<NPCAIController> hidingNpcs;

    private BoxCollider col;
    private void Start()
    {
        col= GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered collider " + other.name);
        if(other.gameObject.transform.parent.TryGetComponent(out NPCAIController npc))
        {
            if (!npc.IsTarget(transform)) return;
            if (isHidingSpot && npc.CanHide() && TryHide(npc))
            {
                StartCoroutine(npc.ScheduleDeactivation(deactivationDelay));
            }
            else
            {
                _ta[0] = this;
                npc.ChangeTarget(WaypointManager.instance.GetRandomWaypoint(_ta).transform);
            }
        }
    }

    public void ScareNpcs(Vector3 bullethitpos)
    {
        if (Vector3.Distance(bullethitpos, transform.position) > scareRadius) return;
       foreach(NPCAIController npc in hidingNpcs)
        {
            npc.ChangeTarget(WaypointManager.instance.GetRandomWaypoint(_ta).transform);
        }
       hidingNpcs.Clear();
        StartCoroutine(Cooldown());
        WaypointManager.instance.AddWaypoint(this);
    }

    private IEnumerator Cooldown()
    {
        cooldDown = true;
        yield return new WaitForSeconds(hideCooldown);
        cooldDown = false;
    }

    private bool TryHide(NPCAIController npc)
    {
        if(cooldDown) return false;
        if(!npc.CanHide() || hidingNpcs.Count == capacity) return false;
        if (hidingNpcs != null && hidingNpcs.Count < capacity)
        {
            hidingNpcs.Add(npc);
            if(hidingNpcs.Count == capacity)
                WaypointManager.instance.RemoveWaypoint(this);
            return true;
        }
        return false;
    }

    public bool IsHidingSpot()
    {
        return isHidingSpot;
    }

#if UNITY_EDITOR
    [SerializeField] private bool drawDebugArea;
    private void OnDrawGizmosSelected()
    {
        if(col == null) col = GetComponent<BoxCollider>();
        if (drawDebugArea)
        {
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawCube(transform.position, col.size);

            Gizmos.color = new Color(1, 0, 0, 1f);
            Gizmos.DrawWireSphere(transform.position, scareRadius);
            Gizmos.color = new Color(1, 0, 0, 0.2f);
            Gizmos.DrawSphere(transform.position, scareRadius);
        }
    }
#endif
}
