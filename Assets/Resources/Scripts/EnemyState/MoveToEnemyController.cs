using UnityEngine;
using System.Collections;

public abstract class MoveToEnemyController : EnemyController {
    public const string NO_TARGET_STATE = "no target";
    public const string MOVE_TO_WALKING_STATE = "walking";
    public const string MOVE_TO_NAVIGATING_STATE = "navigating";

    public string moveToState;
    private Vector3 moveToTarget;

    protected void DoMoveToUpdate() {
        switch(moveToState) {
            case MOVE_TO_WALKING_STATE:
                UpdateWalking();
                break;

            case MOVE_TO_NAVIGATING_STATE:
                UpdateNavigating();
                break;
        }
    }

    //copied out of a misguided desire to prematurely optimize
    protected void MoveToTarget(Vector3 target) {
        var collider = GetComponent<Collider2D>();
        var dir = target - transform.position;

        int wallLayerMask = LayerMask.GetMask("Wall");
        var hit = Physics2D.BoxCast(transform.position, collider.bounds.size, 0f, Vector3.Normalize(dir), Vector3.Magnitude(dir), wallLayerMask);
        if (hit.collider == null) {
            StartWalkStraightTo(target);
        } else {
            StartNavigateTo(target);
        }
    }

    protected void MoveToTarget(GameObject target) {
        var collider = GetComponent<Collider2D>();
        var dir = target.transform.position - transform.position;

        int wallLayerMask = LayerMask.GetMask("Wall");
        var hit = Physics2D.BoxCast(transform.position, collider.bounds.size, 0f, Vector3.Normalize(dir), Vector3.Magnitude(dir), wallLayerMask);
        if (hit.collider == null) {
            StartWalkStraightTo(target);
        } else {
            StartNavigateTo(target);
        }
    }

    protected virtual void StartWalkStraightTo(GameObject target) {
        moveToTarget = target.transform.position;
        moveToState = MOVE_TO_WALKING_STATE;
    }

    protected virtual void StartNavigateTo(GameObject target) {
        moveToTarget = target.transform.position;
        navigation.MoveTo(target, GetStateSpeed());
        moveToState = MOVE_TO_NAVIGATING_STATE;
    }

    protected virtual void StartWalkStraightTo(Vector3 target) {
        moveToTarget = target;
        moveToState = MOVE_TO_WALKING_STATE;
    }

    protected virtual void StartNavigateTo(Vector3 target) {
        moveToTarget = target;
        navigation.MoveTo(target, GetStateSpeed());
        moveToState = MOVE_TO_NAVIGATING_STATE;
    }

    protected abstract void OnMoveToComplete();

    protected virtual void StopMoveTo() {
        if(moveToState == MOVE_TO_NAVIGATING_STATE) {
            navigation.Stop();
            moveToState = NO_TARGET_STATE;
            moveToTarget = Vector3.zero;
        }
    }

    protected virtual void UpdateNavigating() {
        if (TestAtTarget()) {
            navigation.Stop();
            moveToState = NO_TARGET_STATE;
            OnMoveToComplete();
        }
    }
    
    protected virtual void UpdateWalking() {
        float step = GetStateSpeed() * Time.deltaTime;
        rbody.MovePosition(Vector2.MoveTowards(transform.position, moveToTarget, step));

        if(TestAtTarget()) {
            moveToState = NO_TARGET_STATE;
            OnMoveToComplete();
        }
    }

    protected bool TestAtTarget() {
        var dist = Vector2.Distance(transform.position, moveToTarget);
        //Debug.Log(string.Format("{0}: Distance from Target -- {1}", enemy.name, dist));
        return dist < 0.15f; //TODO make this a variable
    }

    public void OnDrawGizmos() {
        if(moveToState != NO_TARGET_STATE) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, moveToTarget);
            Gizmos.color = Color.black;
            Gizmos.DrawCube(moveToTarget, new Vector3(0.25f, 0.25f, 1f));
        }
    }
}
