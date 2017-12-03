using UnityEngine;
using System.Collections;

public abstract class MoveToEnemyState : AEnemyState {
    public const string NO_TARGET_STATE = "no target";
    public const string MOVE_TO_WALKING_STATE = "walking";
    public const string MOVE_TO_NAVIGATING_STATE = "navigating";

    protected string moveToState;
    protected GameObject moveToTarget;

    protected void DoMoveToUpdate(EnemyController enemy) {
        switch(moveToState) {
            case MOVE_TO_WALKING_STATE:
                UpdateWalking(enemy);
                break;

            case MOVE_TO_NAVIGATING_STATE:
                UpdateNavigating(enemy);
                break;
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

    protected virtual void StartWalkStraightTo(EnemyController enemy, GameObject target) {
        if(Debug.isDebugBuild) {
            Debug.Log(string.Format("{0}: Starting walk straight to {1}", enemy.name, target.name));
        }

        moveToTarget = target;
        moveToState = MOVE_TO_WALKING_STATE;
    }

    protected virtual void StartNavigateTo(EnemyController enemy, GameObject target) {
        if(Debug.isDebugBuild) {
            Debug.Log(string.Format("{0}: Starting navigate to {1}", enemy.name, target.name));
        }

        moveToTarget = target;
        enemy.navigation.MoveTo(target, GetStateSpeed());
        moveToState = MOVE_TO_NAVIGATING_STATE;
    }

    protected abstract void OnMoveToComplete(EnemyController enemy);

    protected virtual void StopMoveTo(EnemyController enemy) {
        if(moveToState == MOVE_TO_NAVIGATING_STATE) {
            enemy.navigation.Stop();
            moveToState = NO_TARGET_STATE;
        }
    }

    protected virtual void UpdateNavigating(EnemyController enemy) {
        if (TestAtTarget(enemy)) {
            enemy.navigation.Stop();
            moveToState = NO_TARGET_STATE;
            OnMoveToComplete(enemy);
        }
    }

    protected virtual void UpdateWalking(EnemyController enemy) {
        float step = GetStateSpeed() * Time.deltaTime;
        enemy.rbody.MovePosition(Vector2.MoveTowards(enemy.transform.position, moveToTarget.transform.position, step));

        if(TestAtTarget(enemy)) {
            moveToState = NO_TARGET_STATE;
            OnMoveToComplete(enemy);
        }
    }

    protected bool TestAtTarget(EnemyController enemy) {
        var dist = Vector2.Distance(enemy.transform.position, moveToTarget.transform.position);
        //Debug.Log(string.Format("{0}: Distance from Target -- {1}", enemy.name, dist));
        return dist < 0.15f; //TODO make this a variable
    }
}
