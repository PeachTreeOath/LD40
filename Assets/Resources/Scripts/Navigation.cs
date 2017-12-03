using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

    private Rigidbody2D rBody;

    private GameObject currentTarget;
    private GameObject currentWaypoint;
    private GameObject lastWaypoint;
    public float waypointReachedthreshold = 0.1f;
    public float targetReachedthreashold = 0.75f;
    private bool pathBuilt = false;
    private bool allowedMovement = false;
    private float currentMovementSpeed;

    GameObject gridWaypoints;

	// Use this for initialization
	void Start () {

        rBody = GetComponent<Rigidbody2D>();
        gridWaypoints = GameObject.Find("Waypoints");
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
        currentWaypoint = null;
        currentTarget = null;
    }

    public void MoveTo(GameObject target, float movementSpeed)
    {
        allowedMovement = true;
        currentTarget = target;
        currentMovementSpeed = movementSpeed;
        //Find nearest waypoint. Update statement will begin to move to the

        FindClosestWaypoint();
        
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
        Vector2 direction = (Vector2)currentWaypoint.transform.position - (Vector2)transform.position;
        
        rBody.MovePosition(rBody.position + (direction.normalized * (currentMovementSpeed * Time.deltaTime)));
        
    }

    void UpdatePath()
    {
        if (currentWaypoint != null && pathBuilt)
        {
            //Determine if the distance to the current waypoint is less than the threshold value.
            //If it is then get the next waypoint.
            if (Vector2.Distance(transform.position, currentWaypoint.transform.position)
                <= waypointReachedthreshold)
            {
                GetNextWaypoint();
            }
        }
    }

    void FindClosestWaypoint()
    {
        foreach (Transform waypoint in gridWaypoints.transform)
        {
            if (currentWaypoint == null ||
                Vector2.Distance(waypoint.position, transform.position)
                < Vector2.Distance(currentWaypoint.transform.position, transform.position))
            {
                currentWaypoint = waypoint.gameObject;
            }
        }
    }

    void GetNextWaypoint()
    {
        Waypoint waypoint = currentWaypoint.GetComponent<Waypoint>();
        GameObject newWaypoint = null;
        
        foreach (GameObject neighbor in waypoint.neighbors)
        {
            //Determine which neighbor is closest to the target.
            if (newWaypoint == null ||
                Vector2.Distance(neighbor.transform.position, currentTarget.transform.position)
                < Vector2.Distance(newWaypoint.transform.position, currentTarget.transform.position))
            {

                if (lastWaypoint == null || lastWaypoint.transform.position != neighbor.transform.position)
                {
                    newWaypoint = neighbor;
                }

            }
        }
        lastWaypoint = currentWaypoint;
        currentWaypoint = newWaypoint;
    }
}
