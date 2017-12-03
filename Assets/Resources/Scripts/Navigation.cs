using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

    private Rigidbody2D rBody;

    private GameObject currentTarget;
    private int currentPathIndex;
    private GameObject lastWaypoint;
    public float waypointReachedthreshold = 0.1f;
    public float targetReachedthreashold = 0.75f;
    private bool pathBuilt = false;
    private bool allowedMovement = false;
    private float currentMovementSpeed;

    GameObject gridWaypoints;

    List<GameObject> path;

	// Use this for initialization
	void Start () {

        rBody = GetComponent<Rigidbody2D>();
        gridWaypoints = GameObject.Find("Waypoints" + GlobalPersistentStats.instance.level);
	}
	
	// Update is called once per frame
	void Update () {
        if (allowedMovement)
        {
            PerformMovement();
            UpdatePath();
            if (HasReachedTarget())
            {
                Stop();
            }
            
        }
	}

    // WARNING! Trying to understand what happens beyond this point may cause insanity.
    // Good luck soldier...

    public void Stop()
    {
        //Stop all movement by disallowing movment and resetting the current waypoint and target.
        allowedMovement = false;
        currentPathIndex = -1;
        currentTarget = null;
    }

    public void MoveTo(GameObject target, float movementSpeed)
    {
        allowedMovement = true;
        currentTarget = target;
        currentMovementSpeed = movementSpeed;
        path = Pathfinding.FindPath(gameObject, target);
        currentPathIndex = 0;
        pathBuilt = true;
    }

    public bool HasTarget()
    {
        return currentTarget != null;
    }

    public bool HasReachedTarget()
    {
        return Vector2.Distance(transform.position, currentTarget.transform.position) 
            < targetReachedthreashold;
    }

    public float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, currentTarget.transform.position);
    }

    void PerformMovement()
    {
        Vector2 position = currentPathIndex >= path.Count ? (Vector2)currentTarget.transform.position : (Vector2)path[currentPathIndex].transform.position;
        Vector2 direction = position - (Vector2)transform.position;
        
        rBody.MovePosition(rBody.position + (direction.normalized * (currentMovementSpeed * Time.deltaTime)));
    }

    void UpdatePath()
    {
        if(currentPathIndex >= path.Count) {
            return;
        }

        //Determine if the distance to the current waypoint is less than the threshold value.
        //If it is then get the next waypoint.
        if (Vector2.Distance(transform.position, path[currentPathIndex].transform.position)
            <= waypointReachedthreshold)
        {
            currentPathIndex++;
        }
    }
}
