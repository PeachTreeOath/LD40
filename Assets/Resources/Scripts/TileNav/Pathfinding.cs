using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding {

    public static List<Vector3> FindPath(GameObject start, Vector3 end) {
        var realStart = start.GetComponent<Waypoint>();
        if (realStart == null) {
            realStart = Waypoints.FindNearestWaypoint(start);
        }

        var realEnd = Waypoints.FindNearestWaypoint(end);

        return BreadthFirstSearch(realStart, realEnd);
    }

    public static List<Vector3> FindPath(GameObject start, GameObject end) {
        var realStart = start.GetComponent<Waypoint>();
        var realEnd = end.GetComponent<Waypoint>();

        if (realStart == null) {
            realStart = Waypoints.FindNearestWaypoint(start);
        }

        if (realEnd == null) {
            realEnd = Waypoints.FindNearestWaypoint(end);
        }

        return BreadthFirstSearch(realStart, realEnd);
    }

    public static List<Vector3> BreadthFirstSearch(Waypoint start, Waypoint end) {
        Queue<Waypoint> openList = new Queue<Waypoint>();
        HashSet<Waypoint> openSet = new HashSet<Waypoint>();
        HashSet<Waypoint> closedSet = new HashSet<Waypoint>();
        Dictionary<Waypoint, Waypoint> trace = new Dictionary<Waypoint, Waypoint>();
        Dictionary<Waypoint, float> gScore = new Dictionary<Waypoint, float>();

        openList.Enqueue(start);
        openSet.Add(start);
        gScore[start] = 0;

        while(openList.Count > 0) {
            var current = openList.Dequeue();

            if(current == end) {
                return ReconstructPath(trace, end);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach(var neighbor in current.neighbors) {
                var waypoint = neighbor.GetComponent<Waypoint>(); //TODO this is bad...
                if(closedSet.Contains(waypoint)) {
                    continue;
                }

                if(!openSet.Contains(waypoint)) {
                    openList.Enqueue(waypoint);
                    openSet.Add(waypoint);
                    
                    trace[waypoint] = current;
                    gScore[waypoint] = gScore[current] + 1;
                } else {
                    var newScore = gScore[current] + 1;
                    if(newScore >= gScore[waypoint]) {
                        continue;
                    }

                    trace[waypoint] = current;
                    gScore[waypoint] = newScore;
                }
            }
        }

        return new List<Vector3>();
    }

    //TODO path current does world position only
    protected static List<Vector3> ReconstructPath(Dictionary<Waypoint, Waypoint> trace, Waypoint goal) {
        List<Vector3> path = new List<Vector3>();

        var next = goal;

        path.Add(next.gameObject.transform.position);
        while(trace.ContainsKey(next)) {
            next = trace[next];
            path.Add(next.gameObject.transform.position);
        }

        path.Reverse();
        return path;
    }
}
