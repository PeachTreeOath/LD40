using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public List<GameObject> neighbors;

    private void OnDestroy() {
        foreach(var neighbor in neighbors) {
            var waypoint = neighbor.GetComponent<Waypoint>();
            waypoint.neighbors.Remove(gameObject);
        }
    }
}
