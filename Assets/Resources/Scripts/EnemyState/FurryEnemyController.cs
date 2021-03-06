﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurryEnemyController : WandererEnemyController {

    const string HOLDING_HANDS_STATE = "holding hands";

    public override float GetStateSpeed()
    {
        return 1.0f;
    }

    public override void UpdateStates() {
        switch(state) {
            case HOLDING_HANDS_STATE:
                ChangeFavor();
                break;

            default:
                base.UpdateStates();
                break;
        }
    }

    public void StartHoldingHands() {
        StopMoveTo();
        state = HOLDING_HANDS_STATE;
        AudioManager.instance.PlaySound("hand_holding");
    }

    public void StopHoldingHands() {
        StartWander(); 
    }

}
