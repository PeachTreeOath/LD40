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
            FootballGO = player.FootballGO;

            Vector2 direction = player.GetPlayerDirection();
            FootballGO.SetActive(true);
            FootballGO.transform.localPosition = player.transform.localPosition;

            if (FootballGO != null)
            {
                FootballPickup football = FootballGO.GetComponent<FootballPickup>();
                football.ThrowFootball(direction * speed);
                player.hasBall = false;
            }
        }
    }
}
