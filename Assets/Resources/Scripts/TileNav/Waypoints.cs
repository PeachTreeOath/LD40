using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints {

    const float NEARBY_DISTANCE = 1f;

    public static List<Waypoint> FindNearbyWaypoints(Vector3 position, float radius=NEARBY_DISTANCE, bool sort=false) {
        int layerMask = LayerMask.GetMask("Navigation");
        var colliders = Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), radius, layerMask);

        List<Waypoint> waypoints = new List<Waypoint>();
        foreach(var collider in colliders) {
            var waypoint = collider.GetComponent<Waypoint>();
            if(waypoint) {
                waypoints.Add(waypoint);
            }
        }

        //TODO is this right?
        if(sort) {
            waypoints.Sort((a, b) => {
                var aDist = Vector2.Distance(a.gameObject.transform.position, position);
                var bDist = Vector2.Distance(b.gameObject.transform.position, position);

                if( Mathf.Approximately(aDist, bDist) ) {
                    return 0;
                } else {
                    return aDist > bDist ? 1 : -1;
                }
            });
        }

        return waypoints;
    }

    public static Waypoint FindNearestWaypoint(Vector3 position) {
        var waypoints = FindNearbyWaypoints(position, NEARBY_DISTANCE, true);
        return waypoints.Count > 0 ? waypoints[0] : null;
    }

    public static Waypoint FindNearestWaypoint(GameObject gameObject) {
        return FindNearestWaypoint(gameObject.transform.position);
    }
}
