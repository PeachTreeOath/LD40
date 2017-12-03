using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

    private Rigidbody2D rBody;

    private Vector3 currentTarget;
    private int currentPathIndex;
    private bool allowedMovement = false;
    private float currentMovementSpeed;

    List<Vector3> path;

    public float waypointReachedthreshold = 0.1f;
    public float targetReachedthreashold = 0.75f;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
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
    }

    public void MoveTo(Vector3 target, float movementSpeed) {
        //TODO this copy pasta is bad >_>
        allowedMovement = true;
        currentMovementSpeed = movementSpeed;
        path = Pathfinding.FindPath(gameObject, target);
        currentPathIndex = 0;
        currentTarget = target;
    }

    public void MoveTo(GameObject target, float movementSpeed)
    {
        allowedMovement = true;
        currentMovementSpeed = movementSpeed;
        path = Pathfinding.FindPath(gameObject, target);
        currentPathIndex = 0;
        currentTarget = target.transform.position;
    }

    public bool HasTarget()
    {
        return allowedMovement;
    }

    public bool HasReachedTarget()
    {
        return Vector2.Distance(transform.position, currentTarget) 
            < targetReachedthreashold;
    }

    public float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, currentTarget);
    }

    void PerformMovement()
    {
        Vector2 position = currentPathIndex >= path.Count ? (Vector2)currentTarget : (Vector2)path[currentPathIndex];
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
        if (Vector2.Distance(transform.position, path[currentPathIndex])
            <= waypointReachedthreshold)
        {
            currentPathIndex++;
        }
    }
}
