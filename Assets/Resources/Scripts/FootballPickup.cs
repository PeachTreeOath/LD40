using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballPickup : MonoBehaviour {

    private Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            PlayerController player = PlayerController.instance;
            player.hasBall = true;
            gameObject.SetActive(false);
        }
    }

    public void ThrowFootball(Vector2 force)
    {
        rbody.MovePosition(force * 0.2f);
        rbody.AddForce(force, ForceMode2D.Impulse);
    }
}
