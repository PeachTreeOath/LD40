using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurryStateBehaviour : APlayerBehaviour
{
    private ContactFilter2D filter = new ContactFilter2D();

    public override void ExecuteBehaviourAction()
    {
        PlayerController player = PlayerController.instance;
        if (!player.HasHostage()) {
            TakeHostage();
        }
        else {
            ReleaseHostage();
        }
    }

    protected void TakeHostage()
    {
        GameObject enemyGo = AttemptToGrabHands();
        if (enemyGo != null) {
            var furry = enemyGo.GetComponent<FurryEnemyController>();
            furry.StartHoldingHands();

            //I'd rather you weren't here to see this
            furry.transform.SetParent(PlayerController.instance.transform);
            var joint = PlayerController.instance.gameObject.AddComponent<FixedJoint2D>();
            var furryFurryBody = furry.GetComponent<Rigidbody2D>();
            joint.connectedBody = furryFurryBody;
        }
    }

    protected void ReleaseHostage() {
        var joint = PlayerController.instance.GetComponent<FixedJoint2D>();
        GameObject.Destroy(joint);

        var furry = PlayerController.instance.GetComponentInChildren<FurryEnemyController>();
        furry.transform.SetParent(null);
        furry.StopHoldingHands();

    }

    protected GameObject AttemptToGrabHands() {
        GameObject enemyGo = null;

        PlayerController player = PlayerController.instance;
        Collider2D col = player.GetComponent<Collider2D>();
        if (col != null) {
            Collider2D[] colliders = new Collider2D[10];
            col.OverlapCollider(filter, colliders);
            foreach (Collider2D collider in colliders) {
                if (collider != null && collider.gameObject.tag.Equals("Furry")) {
                    enemyGo = collider.gameObject;
                    break;
                }
            }
        }

        return enemyGo;
    }
}
