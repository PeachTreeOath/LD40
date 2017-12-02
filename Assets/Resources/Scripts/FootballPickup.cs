using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballPickup : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            PlayerController player = PlayerController.instance;
            player.hasBall = true;
            this.gameObject.SetActive(false);
        }

    }
}
