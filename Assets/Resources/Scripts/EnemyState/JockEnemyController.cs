using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO fetch wait
//TODO fetch cooldown (fetch time + wait)
public class JockEnemyController : WandererEnemyController {
    const string FETCHING_STATE = "fetching";

    private Football football;

    public float footballChaseRadius = 15f;
    public float footballScatterRadius = 1f;

    protected override void Start() {
        base.Start();

        var footballGO = GameObject.FindGameObjectWithTag("Football");
        football = footballGO.GetComponent<Football>();

        football.lureEvent.AddListener(OnLureActive);
    }

    public override void UpdateStates() {
        switch(state) {
            case FETCHING_STATE:
                UpdateFetching();
                break;

            default:
                base.UpdateStates();
                break;
        }
    }

    protected virtual void StartFetching() {
        StopMoveTo();
        Vector3 position = (Vector2)football.transform.position + (Random.insideUnitCircle * footballScatterRadius);
        MoveToTarget(position);
        state = FETCHING_STATE;
    }

    protected virtual void UpdateFetching() {

    }

    protected override void OnMoveToComplete() {
        if(state == FETCHING_STATE) {
            
        } else {
            base.OnMoveToComplete();
        }
    }

    protected virtual void OnLureActive(string message) {
        switch(message) {
            case LureEvent.ATTRACT:
                if (CanSeeFootball()) {
                    StartFetching();
                    AudioManager.instance.PlaySound("aggro", 0.3f);
                }
                break;

            case LureEvent.CANCEL:
                if(state == FETCHING_STATE) {
                    StopMoveTo();
                    StartWander(); //TODO is this right?
                }
                break;
        }
    }

    protected bool CanSeeFootball() {
        int layerMask = LayerMask.GetMask("Wall");
        Vector2 dir = football.transform.position - transform.position;
        float dist = Mathf.Min(Vector3.Magnitude(dir), footballChaseRadius);

        var hit = Physics2D.Raycast(transform.position, Vector3.Normalize(dir), dist, layerMask);
        return hit.collider == null;
    }

    private void OnDestroy() {
       if(football) {
            football.lureEvent.RemoveListener(OnLureActive);
       } 
    }

    public override float GetStateSpeed() {
        return state == FETCHING_STATE ? 6f : 3f;
    }

}
