using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WaypointManager instance;

    [SerializeField] private List<WaypointController> waypoints = new List<WaypointController>();
    [SerializeField] private List<WaypointController> _disabledWaypoints = new List<WaypointController>();
    private void Awake()
    {
        instance = this;
        waypoints.AddRange(FindObjectsByType<WaypointController>(FindObjectsSortMode.None));
    }
    
    public void CheckScares(Vector3 point)
    {
        List<WaypointController> wps = new List<WaypointController>();
        foreach(WaypointController w in waypoints)
        {
            if (w.IsHidingSpot())
            {
                wps.Add(w);
            }
        }
        foreach (WaypointController w in _disabledWaypoints)
        {
            if (w.IsHidingSpot())
            {
                wps.Add(w);
            }
        }
        foreach (WaypointController w in wps)
        {
            w.ScareNpcs(point);
        }
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
        _disabledWaypoints.Add(wp);
    }
    public void AddWaypoint(WaypointController wp)
    {
        if(waypoints.Contains(wp)) return;
        waypoints.Add(wp);
        if(_disabledWaypoints.Contains(wp)) _disabledWaypoints.Remove(wp);
    }
}
