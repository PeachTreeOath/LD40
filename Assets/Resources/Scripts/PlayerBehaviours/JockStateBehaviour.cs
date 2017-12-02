using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockStateBehaviour : APlayerBehaviour
{
    /// <summary>
    /// The Gameobject for the football.
    /// </summary>
    private GameObject FootballGO;

    /// <summary>
    /// Speed at which the ball flies.
    /// </summary>
    private float speed = 5f;

    /// <summary>
    /// Throw the Foosball
    /// </summary>
    public override void ExecuteBehaviourAction()
    {
        PlayerController player = PlayerController.instance;
        if (player.hasBall)
        {
            if (FootballGO == null)
                GameObject.FindGameObjectWithTag("Football");

            Vector2 direction = player.GetPlayerDirection();
            FootballGO.transform.localPosition = player.transform.localPosition;

            if (FootballGO != null)
            {
                FootballGO.transform.Translate(direction * speed);
                player.hasBall = false;
            }
        }
    }
}
