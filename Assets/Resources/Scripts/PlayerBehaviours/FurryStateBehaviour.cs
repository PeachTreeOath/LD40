using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurryStateBehaviour : APlayerBehaviour
{
    /// <summary>
    /// Grab the nearest Furry and drag them along with you.
    /// </summary>
    public override void ExecuteBehaviourAction()
    {
        PlayerController player = PlayerController.instance;
        GameObject enemyGo = null;
        Collider2D col = player.GetComponent<Collider2D>();
        if(col != null)
        {
            if (player.canGrabTail)
            {
                Collider2D[] colliders = new Collider2D[10];
                ContactFilter2D filter = new ContactFilter2D();
                col.OverlapCollider(filter, colliders);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.tag.Equals("Furry"))
                    {
                        enemyGo = collider.gameObject;
                    }

                }

                //Parent the furry to the player.
                if (enemyGo != null)
                {
                    enemyGo.transform.SetParent(player.transform);
                    player.canGrabTail = false;
                    //Stop the furries movement animation.

                }
            }
            else
            {
                //Parent the furry back onto the scene.
               if(enemyGo != null)
                {
                    enemyGo.transform.SetParent(player.transform.parent);
                    player.canGrabTail = true;
                    //Start the furries movement animation.
                }
            }

        }
    }
}
