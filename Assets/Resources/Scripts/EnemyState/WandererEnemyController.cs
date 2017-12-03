using UnityEngine;
using System.Collections;

public abstract class WandererEnemyController : MoveToEnemyController {
    public const string START_STATE = "start";
    public const string WALKING_STATE = "walking";
    public const string WAITING_STATE = "waiting";

    public float wanderTargetRadius = 5f;
    public float minWanderWait = 0f;
    public float maxWanderWait = 3.0f;

    protected string state = START_STATE;
    protected GameObject wanderTarget;
    protected float waitTimer;

    //Debug code
    public GameObject tempTarget;
    //end debug code

    public void Update() {
        UpdateStates();
        DoMoveToUpdate();
    }

    public virtual void UpdateStates() {
        switch(state) {
            case START_STATE:
                StartWander();
                break;

            case WALKING_STATE:
                DoMoveToUpdate();
                break;

            case WAITING_STATE:
                UpdateWaiting();
                break;
        }
    }

    protected override void OnMoveToComplete() {
        StartWaiting();
    }

    protected virtual void UpdateWaiting() {
        waitTimer -= Time.deltaTime; 

        if(waitTimer <= 0) {
            StartWander();
        }
    }

    protected virtual void StartWander() {
        var waypoint = ChooseNearbyWaypointAtRandom();
        //Debug code
        Destroy(tempTarget);
        GameObject go = Instantiate<GameObject>(waypoint);
        DrawGizmo gizmo = go.AddComponent<DrawGizmo>();
        gizmo.entity = gameObject;
        tempTarget = go;
        //end debug code
        MoveToTarget(waypoint);
        state = WALKING_STATE;
    }

    protected void StartWaiting() {
        waitTimer = Random.Range(minWanderWait, maxWanderWait);
        state = WAITING_STATE;
    }

    protected GameObject ChooseNearbyWaypointAtRandom() {
        var waypoints = Waypoints.FindNearbyWaypoints(transform.position, wanderTargetRadius);
        var index = Random.Range(0, waypoints.Count);

        return waypoints[index].gameObject;
    }
    
}
