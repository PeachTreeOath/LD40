using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballPickup : MonoBehaviour
{
    private Football football;

    public float throwTime;
    public float throwTimeDeltaNeededForPickup = 1f;

    public void Start() {
        football = transform.parent.GetComponent<Football>(); 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (Time.time > throwTimeDeltaNeededForPickup + throwTime)
            {
                if (PlayerStateController.instance.CurrentState == CliqueEnum.JOCK)
                {
                    PlayerController player = PlayerController.instance;
                    player.hasBall = true;
                    football.OnPickUp();
                }
            }
        }
    }

}
