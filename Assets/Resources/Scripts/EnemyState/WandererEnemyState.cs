using UnityEngine;
using System.Collections;

public abstract class WandererEnemyState : AEnemyState {
    public float wanderTargetRadius = 5f;
    public float minWanderWait = 0.5f;
    public float maxWanderWait = 1.5f;

    public enum WanderState { Start, Walking, Navigating, Waiting };


    protected WanderState wanderState = WanderState.Start;
    protected GameObject wanderTarget;
    protected float waitTimer;

    public override void DoUpdate(EnemyController enemy) {
        switch(wanderState) {
            case WanderState.Start:
                StartWander(enemy);
                break;

            case WanderState.Walking:
                UpdateWalking(enemy);
                break;

            case WanderState.Navigating:
                UpdateNavigating(enemy);
                break;

            case WanderState.Waiting:
                UpdateWaiting(enemy);
                break;
        } 
    }

    protected virtual void UpdateNavigating(EnemyController enemy) {
        if (TestAtTarget(enemy)) {
            enemy.navigation.Stop();
            StartWaiting(enemy);
        }
    }

    protected virtual void UpdateWalking(EnemyController enemy) {
        float step = GetStateSpeed() * Time.deltaTime;
        enemy.rbody.MovePosition(Vector2.MoveTowards(enemy.transform.position, wanderTarget.transform.position, step));

        if(TestAtTarget(enemy)) {
            StartWaiting(enemy);
        }
    }

    protected virtual void UpdateWaiting(EnemyController enemy) {
        waitTimer -= Time.deltaTime; 
        if(waitTimer <= 0) {
            StartWander(enemy);
        }
    }

    protected void MoveToTarget(EnemyController enemy, GameObject target) {
        var collider = enemy.GetComponent<Collider2D>();
        var dir = target.transform.position - enemy.transform.position;

        int wallLayerMask = LayerMask.GetMask("Wall");
        var hit = Physics2D.BoxCast(enemy.transform.position, collider.bounds.size, 0f, Vector3.Normalize(dir), Vector3.Magnitude(dir), wallLayerMask);
        if (hit.collider == null) {
            StartWalkStraightTo(enemy, target);
        } else {
            StartNavigateTo(enemy, target);
        }
    }

    protected virtual void StartWander(EnemyController enemy) {
        if(Debug.isDebugBuild) {
            Debug.Log(string.Format("{0}: Starting wander", enemy.gameObject.name));
        }

        var waypoint = ChooseNearbyWaypointAtRandom(enemy);
        MoveToTarget(enemy, waypoint);
    }

    protected virtual void StartWalkStraightTo(EnemyController enemy, GameObject target) {
        if(Debug.isDebugBuild) {
            Debug.Log(string.Format("{0}: Starting walk straight to {1}", enemy.name, target.name));
        }

        wanderTarget = target;
        wanderState = WanderState.Walking;
    }

    protected virtual void StartNavigateTo(EnemyController enemy, GameObject target) {
        if(Debug.isDebugBuild) {
            Debug.Log(string.Format("{0}: Starting navigate to {1}", enemy.name, target.name));
        }

        wanderTarget = target;
        enemy.navigation.MoveTo(target, GetStateSpeed());
        wanderState = WanderState.Navigating;
    }

    protected void StartWaiting(EnemyController enemy) {
        if(Debug.isDebugBuild) {
            Debug.Log(string.Format("{0}: Starting waiting", enemy.gameObject.name));
        }

        waitTimer = Random.Range(minWanderWait, maxWanderWait);
        wanderState = WanderState.Waiting;
    }

    protected bool TestAtTarget(EnemyController enemy) {
        var dist = Vector2.Distance(enemy.transform.position, wanderTarget.transform.position);
        //Debug.Log(string.Format("{0}: Distance from Target -- {1}", enemy.name, dist));
        return dist < 0.15f; //TODO make this a variable
    }

    protected GameObject ChooseNearbyWaypointAtRandom(EnemyController enemy) {
        var waypoints = Waypoints.FindNearbyWaypoints(enemy.transform.position, wanderTargetRadius);
        var index = Random.Range(0, waypoints.Count);

        return waypoints[index].gameObject;
    }
}
