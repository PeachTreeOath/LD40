using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurryEnemyController : WandererEnemyController {

    const string HOLDING_HANDS_STATE = "holding hands";

    private FixedJoint2D handJoint;

    protected override void Start() {
        base.Start();
        handJoint = GetComponent<FixedJoint2D>();
    }

    public override float GetStateSpeed()
    {
        return 1.0f;
    }

    public override void UpdateStates() {
        switch(state) {
            case HOLDING_HANDS_STATE:
                break;

            default:
                base.UpdateStates();
                break;
        }
    }

    public void StartHoldingHands() {
        StopMoveTo();
        transform.SetParent(PlayerController.instance.transform);
        handJoint.connectedBody = PlayerController.instance.GetComponent<Rigidbody2D>();
        state = HOLDING_HANDS_STATE;
    }

    public void StopHoldingHands() {
        transform.SetParent(null);
        handJoint.connectedBody = null;
        StartWander(); 
    }

}
