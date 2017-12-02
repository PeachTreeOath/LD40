using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

    private Rigidbody2D rBody;

    private GameObject currentTarget;
    private GameObject currentWaypoint;
    private float waypointReachedthreshold = 0.1f;
    private float targetReachedthreashold = 0.75f;
    private float gridSpacing = 1.0f;
    private bool pathBuilt = false;
    private bool allowedMovement = false;
    private float recalculatePathDelay = 1.0f;
    private float lastTimePathRecalculated = 0.0f;
    private float currentMovementSpeed;
    //private bool hasTargetReached = false;

    GameObject gridWaypoints;
    List<GameObject> path = new List<GameObject>();

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
        path.Clear();
    }

    public void MoveTo(GameObject target, float movementSpeed)
    {
        allowedMovement = true;
        currentTarget = target;
        currentMovementSpeed = movementSpeed;
        //Find nearest waypoint. Update statement will begin to move to the

        foreach (Transform waypoint in gridWaypoints.transform)
        {
            if (currentWaypoint == null ||
                Vector2.Distance(waypoint.position, transform.position)
                < Vector2.Distance(currentWaypoint.transform.position, transform.position))
            {
                path.Add(waypoint.gameObject);
                currentWaypoint = waypoint.gameObject;
            }
        }

        StartCoroutine(MoveToCoroutine());
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
       // Debug.Log("enemy is moving");
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

        if (Time.time > lastTimePathRecalculated + recalculatePathDelay)
        {
            StartCoroutine(MoveToCoroutine());
        }
    }

    void GetNextWaypoint()
    {
        Waypoint waypoint = currentWaypoint.GetComponent<Waypoint>();
        GameObject newWaypoint = null;
        foreach (GameObject neighbor in waypoint.neighbors)
        {
            //Determine which neighbor is closest to the target.
            if(newWaypoint == null || 
                Vector2.Distance(neighbor.transform.position,currentTarget.transform.position)
                < Vector2.Distance(newWaypoint.transform.position,currentTarget.transform.position))
            {
                newWaypoint = neighbor;
            }
        }
        currentWaypoint = newWaypoint;
    }

   

    IEnumerator MoveToCoroutine()
    {
        //Build the path.
        float tempDistanceToTarget = 0.0f;
        GameObject closest = currentWaypoint;
        do
        {
            foreach (GameObject neighbor in closest.GetComponent<Waypoint>().neighbors)
            {
                //Determine which if the current neighbor is closer to the target and the start position.
                //If statment breakdown:
                // ClosestWPDistanceToStart + ClosestWPDistanceToTarget 
                // < NeighborWPDistanceToStart + NeighborWPDistanceToTarget 
                // && the neighboor doesn't exist in the path.
                if (Vector2.Distance(closest.transform.position, transform.position)
                    + Vector2.Distance(closest.transform.position, currentTarget.transform.position) <
                    Vector2.Distance(neighbor.transform.position, transform.position)
                    + Vector2.Distance(neighbor.transform.position, currentTarget.transform.position)
                    && !ExistsInPath(neighbor.gameObject))
                {
                    path.Add(neighbor.gameObject);
                    closest = neighbor.gameObject;
                    tempDistanceToTarget =
                        Vector2.Distance(neighbor.transform.position, currentTarget.transform.position);
                }
            }
            yield return null;
        } while (tempDistanceToTarget > gridSpacing);
        pathBuilt = true;
        lastTimePathRecalculated = Time.time;
    }

    private bool ExistsInPath(GameObject wayPoint)
    {
        bool isInPath = false;
        foreach(GameObject pathPoint in path)
        {
            if(pathPoint.transform.position == wayPoint.transform.position)
            {
                isInPath = true;
            }
        }
        return isInPath;
    }
}
