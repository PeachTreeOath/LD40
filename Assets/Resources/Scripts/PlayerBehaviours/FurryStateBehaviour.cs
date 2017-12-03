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
        }
    }

    protected void ReleaseHostage() {
        var furry = PlayerController.instance.GetComponentInChildren<FurryEnemyController>();
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
