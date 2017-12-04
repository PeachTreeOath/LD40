using UnityEngine;
using System.Collections;

public class WaypointGroup : MonoBehaviour {
        
    private void OnDrawGizmosSelected() {
        var waypoints = GetComponentsInChildren<Waypoint>();

        foreach(Waypoint waypoint in waypoints) {
            DrawGizmosForWaypoint(waypoint);
        }
    }

    private void DrawGizmosForWaypoint(Waypoint waypoint) {
        Vector3 position = waypoint.gameObject.transform.position;

        Gizmos.color = Color.blue;
        foreach(var neighbor in waypoint.neighbors) {
            if (neighbor == null) continue;
            Gizmos.DrawLine(position, neighbor.gameObject.transform.position); 
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(position, new Vector3(0.25f, 0.25f, 1f));
    }
}
