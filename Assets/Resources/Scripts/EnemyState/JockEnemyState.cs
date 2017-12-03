using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockEnemyState : WandererEnemyState {
    const string FETCHING_STATE = "fetching";

    private Football football;

    public JockEnemyState() {
        var footballGO = GameObject.FindGameObjectWithTag("Football");
        football = footballGO.GetComponent<Football>();
    }

    public override void UpdateStates(EnemyController enemy) {
        switch(state) {
            //TODO just for testing
            //case START_STATE:
            //    StartFetching(enemy);
            //    break;

            case FETCHING_STATE:
                UpdateFetching(enemy);
                break;

            default:
                base.UpdateStates(enemy);
                break;
        }
    }

    protected virtual void StartFetching(EnemyController enemy) {
        
        state = FETCHING_STATE;
    }

    protected virtual void UpdateFetching(EnemyController enemy) {

    }

    /// <summary>
    /// The sprite the player takes on when changing to this state.
    /// </summary>
    /// <returns></returns>
    public override Sprite GetStateSprite()
    {
        return ResourceLoader.instance.jockSprite;
    }

    public override float GetStateSpeed() {
        return 3f;
    }

}
