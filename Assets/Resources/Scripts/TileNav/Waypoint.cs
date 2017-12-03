using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public List<GameObject> neighbors;

	// Use this for initialization
	

    private void OnDestroy() {
        foreach(var neighbor in neighbors) {
            var waypoint = neighbor.GetComponent<Waypoint>();
            waypoint.neighbors.Remove(gameObject);
        }
    }

    private void OnDrawGizmosSelected() {
        var parent = gameObject.transform.parent.gameObject;
        var waypoints = parent.GetComponentsInChildren<Waypoint>();

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
