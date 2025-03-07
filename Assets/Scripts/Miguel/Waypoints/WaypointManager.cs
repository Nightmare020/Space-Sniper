using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WaypointManager instance;

    [SerializeField] private List<WaypointController> waypoints = new List<WaypointController>();
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        waypoints.AddRange(FindObjectsByType<WaypointController>(FindObjectsSortMode.None));
    }

    public WaypointController GetRandomWaypoint()
    {
        return waypoints.Count > 0 ? waypoints[Random.Range(0, waypoints.Count)] : null;
    }

    public WaypointController GetRandomWaypoint(IEnumerable<WaypointController> excluding)
    {
        List<WaypointController> list = new List<WaypointController>(waypoints);

        if (excluding != null)
            foreach (WaypointController wp in excluding) list.Remove(wp);
        
        return list.Count > 0 ? list[Random.Range(0, list.Count)] : null;
    }

    public void RemoveWaypoint(WaypointController wp)
    {
        waypoints.Remove(wp);
    }
    public void AddWaypoint(WaypointController wp)
    {
        if(waypoints.Contains(wp)) return;
        waypoints.Add(wp);
    }
}
