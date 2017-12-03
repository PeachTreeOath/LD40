using UnityEngine;
using System.Collections;

public abstract class WandererEnemyState : MoveToEnemyState {
    public const string START_STATE = "start";
    public const string WALKING_STATE = "walking";
    public const string WAITING_STATE = "waiting";

    public float wanderTargetRadius = 5f;
    public float minWanderWait = 0f;
    public float maxWanderWait = 3.0f;

    protected string state = START_STATE;
    protected GameObject wanderTarget;
    protected float waitTimer;

    public override void DoUpdate(EnemyController enemy) {
        UpdateStates(enemy);
        DoMoveToUpdate(enemy);
    }

    public virtual void UpdateStates(EnemyController enemy) {
        switch(state) {
            case START_STATE:
                StartWander(enemy);
                break;

            case WALKING_STATE:
                UpdateWalking(enemy);
                break;

            case WAITING_STATE:
                UpdateWaiting(enemy);
                break;
        }
    }

    protected override void OnMoveToComplete(EnemyController enemy) {
        StartWaiting(enemy);
    }

    protected virtual void UpdateWaiting(EnemyController enemy) {
        waitTimer -= Time.deltaTime; 

        if(waitTimer <= 0) {
            StartWander(enemy);
        }
    }

    protected virtual void StartWander(EnemyController enemy) {
        if(Debug.isDebugBuild) {
            Debug.Log(string.Format("{0}: Starting wander", enemy.gameObject.name));
        }

        var waypoint = ChooseNearbyWaypointAtRandom(enemy);
        MoveToTarget(enemy, waypoint);
        state = WALKING_STATE;
    }

    protected void StartWaiting(EnemyController enemy) {
        if(Debug.isDebugBuild) {
            Debug.Log(string.Format("{0}: Starting waiting", enemy.gameObject.name));
        }

        waitTimer = Random.Range(minWanderWait, maxWanderWait);
        state = WAITING_STATE;
    }

    protected GameObject ChooseNearbyWaypointAtRandom(EnemyController enemy) {
        var waypoints = Waypoints.FindNearbyWaypoints(enemy.transform.position, wanderTargetRadius);
        var index = Random.Range(0, waypoints.Count);

        return waypoints[index].gameObject;
    }
}
