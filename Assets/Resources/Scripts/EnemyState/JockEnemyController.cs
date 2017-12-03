using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockEnemyController : WandererEnemyController {
    const string FETCHING_STATE = "fetching";

    private Football football;

    protected override void Start() {
        base.Start();

        var footballGO = GameObject.FindGameObjectWithTag("Football");
        football = footballGO.GetComponent<Football>();

        //football.lureEvent.AddListener()
    }

    public override void UpdateStates() {
        switch(state) {
            //TODO just for testing
            case START_STATE:
                StartFetching();
                break;

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
        MoveToTarget(football.gameObject);
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

    public override float GetStateSpeed() {
        return 3f;
    }

}
